using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public enum InteractionType
{
    Inventory,
    QuickAccess
}

public class NotActionArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InteractionType interactionType;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        if (interactionType == InteractionType.Inventory)
        {
            playerController.InventoryInteraction = true;
        } 
        else if (interactionType == InteractionType.QuickAccess)
        {
            playerController.QuickAccessInteraction = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        if (interactionType == InteractionType.Inventory)
        {
            playerController.InventoryInteraction = false;
        }
        else if (interactionType == InteractionType.QuickAccess)
        {
            playerController.QuickAccessInteraction = false;
        }
    }
}
