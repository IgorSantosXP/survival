using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private MultiAimConstraint headAim;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float retargetSpeed = 5f;
    [SerializeField] private float aimWeightSpeed = 2f;
    [SerializeField] private InventoryController inventory;
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private GameObject handPosition;

    private Vector3 hitPos;
    private Vector3 inputDir;
    private Vector3 moveDir;

    private int inputXHash = Animator.StringToHash("InputX");
    private int inputYHash = Animator.StringToHash("InputY");
    private int punch = Animator.StringToHash("Punch");

    private bool isCombatMode = false;

    private float combatModeTimer = 3f;

    private GameObject equippedItem;
    private GameObject equippedItemSlot;
    private int equippedItemNum;
    

    private void Update()
    {
        SetInput();
        SetDirection();
        SetMovement();
        SetMousePosition();
        SetTargetLook();
        SetCombatMode();
    }

    private void SetInput()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        if (!screenManager.isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isCombatMode = true;
                combatModeTimer = 3f;
                animator.SetTrigger(punch);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckItemAndEquip(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckItemAndEquip(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CheckItemAndEquip(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CheckItemAndEquip(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CheckItemAndEquip(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            CheckItemAndEquip(6);
        }

        inputVector = inputVector.normalized;
        inputDir = new Vector3(inputVector.x, 0f, inputVector.y);
    }

    private void CheckItemAndEquip(int equippedItemNumValue)
    {
        if (equippedItemNum == equippedItemNumValue)
            return;
        equippedItemNum = equippedItemNumValue;
        if (equippedItem != null)
        {
            Destroy(equippedItem);
        }
        if (equippedItemSlot != null)
        {
            equippedItemSlot.SetActive(false);
        }
        if (inventory.QuickAccessContainer.Items[equippedItemNumValue-1].ID >= 0 && inventory.QuickAccessContainer.Items[equippedItemNumValue-1].item.recipe.Length > 0)
        {
            equippedItem = Instantiate(inventory.QuickAccessContainer.Items[equippedItemNumValue-1].item.itemRecipe, handPosition.transform);
            GameObject selectedItem = inventory.QuickAccessContainer.Items[equippedItemNumValue - 1].slotPrefab.GetComponent<QuickAccessController>().SelectedItem;
            selectedItem.SetActive(true);
            equippedItemSlot = selectedItem;
        }
    }

    private void SetMovement()
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        moveDir = cameraForward * inputDir.z + cameraRight * inputDir.x;
        float moveDistance = moveSpeed * Time.deltaTime;
        //float playerRadius = .5f;
        //float playerHeight = 2f;
        //bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //if (!canMove)
        //{
        //    Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        //    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
        //    if (canMove)
        //    {
        //        moveDir = moveDirX;
        //    } else
        //    {
        //        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        //        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

        //        if (canMove)
        //        {
        //            moveDir = moveDirZ;
        //        }
        //    }
        //}
        //if (canMove)
        //{
            transform.position += moveDir * moveDistance;
        //}
    }

    private void SetDirection()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveDir);
        float turnAmount = localMove.x;
        float forwardAmount = localMove.z;

        animator.SetFloat(inputXHash, turnAmount, 0.1f, Time.deltaTime);
        animator.SetFloat(inputYHash, forwardAmount, 0.1f, Time.deltaTime);

        float rotateSpeed = 10f;
        if (!isCombatMode)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        }
    }

    private void SetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            hitPos = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }

    private void SetTargetLook()
    {

        Vector3 delta = hitPos - transform.position;
        float angle = Vector3.Angle(transform.forward, delta);
        float aimWeight = 0;
        Vector3 targetPos = transform.position + (transform.forward * 2f);

        if (angle < maxAngle && !cameraController.isCameraMoving && !isCombatMode)
        {
            targetPos = hitPos;
            aimWeight = 1;
        }

        targetTransform.position = Vector3.Lerp(targetTransform.position, targetPos, Time.deltaTime * retargetSpeed);
        headAim.weight = Mathf.Lerp(headAim.weight, aimWeight, Time.deltaTime * aimWeightSpeed);
    }

    private void SetCombatMode()
    {
        combatModeTimer -= Time.deltaTime;

        if (combatModeTimer <= 0)
        {
            isCombatMode = false;
        }
        if (isCombatMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                hitPos = hit.point;
            }

            Vector3 lookDir = hitPos - transform.position;
            lookDir.y = 0;
            transform.LookAt(transform.position + lookDir, Vector3.up);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[35];
        inventory.QuickAccessContainer.Items = new InventorySlot[6];
    }
}
