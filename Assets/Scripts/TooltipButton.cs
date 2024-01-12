using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TooltipButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private ItemObject itemObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.OnTooltipEnter(itemObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.OnTooltipExit();
    }
}
