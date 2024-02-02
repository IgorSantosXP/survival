using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private CollectableType collectableType;
    private PlayerController playerController;
    private InventoryController inventory;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        inventory = GetComponentInParent<InventoryController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag == "Collectable")
        {
            if (playerController != null && playerController.DetectCollision)
            {
                ICollectable collectable = other.GetComponent<ICollectable>();
                if (collectable != null && collectable.type == collectableType)
                {
                    if (inventory != null)
                    {
                        int resourceAmount = collectable.GetAmount();
                        Debug.Log($"resourceAmount: {resourceAmount}");
                        inventory.AddItem(new Item(collectable.Item), resourceAmount);
                    }
                    collectable.Hit();
                }
            }
            
            
        }
    }
}
