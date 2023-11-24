using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private int _numFound;
    [SerializeField] private InventoryController inventory;

    private readonly Collider[] _colliders = new Collider[3];
    private IInteractable _interactable;
    private InteractionPromptUI _interactionPromptUI;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            _interactionPromptUI = _colliders[0].GetComponentInChildren<InteractionPromptUI>();

            if (_interactable != null)
            {
                
                if (!_interactionPromptUI.IsDisplayed)
                {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interactable.Interact(this);
                    inventory.AddItem(new Item(_interactable.Item), _interactable.GetAmount());
                }
            }
        }
        else
        {
            if (_interactable != null)
            {
                _interactable = null;
                if (_interactionPromptUI != null)
                {
                    _interactionPromptUI.Close();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
