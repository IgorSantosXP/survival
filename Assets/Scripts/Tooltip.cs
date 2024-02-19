using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private RectTransform backgroundRectTransform;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemLevel;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject recipeContainer;
    [SerializeField] private GameObject recipe;
    [SerializeField] private List<GameObject> recipeList = new List<GameObject>();
    [SerializeField] private InventoryController inventory;

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;

        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y - backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height + backgroundRectTransform.rect.height;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    public void ShowTooltip(ItemObject itemObject)
    {
        gameObject.SetActive(true);

        itemName.text = itemObject.Name;
        itemLevel.text = itemObject.Level.ToString();
        itemDescription.text = itemObject.Description;
        itemIcon.sprite = itemObject.uiDisplay;

        for (int i = 0; i < itemObject.recipe.Length; i++)
        {
            GameObject recipeObj = Instantiate(recipe, recipeContainer.transform);
            recipeList.Add(recipeObj);
            RecipeTooltip recipeTooltip = recipeObj.GetComponent<RecipeTooltip>();
            recipeTooltip.recipeImage.sprite = itemObject.recipe[i].resource.uiDisplay;
            recipeTooltip.recipeName.text = itemObject.recipe[i].resource.Name;

            int itemQty = inventory.GetItemAmount(itemObject.recipe[i].resource.Id);

            recipeTooltip.recipeValue.text = $"{itemQty}/{itemObject.recipe[i].value}";
            if (itemQty >= itemObject.recipe[i].value)
            {
                recipeTooltip.recipeValue.color = new Color(125/255f, 255/255f, 76/255f, 255/255f);
                recipeTooltip.recipeName.color = new Color(125/255f, 255/255f, 76/255f, 255/255f);
            }
            else
            {
                recipeTooltip.recipeValue.color = new Color(255/255f, 76/255f, 76/255f, 255/255f);
                recipeTooltip.recipeName.color = new Color(255/255f, 76/255f, 76/255f, 255/255f);
            }
        }
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < recipeList.Count; i++)
        {
            Destroy(recipeList[i]);
        }
    }

    public void OnTooltipEnter(ItemObject itemObject)
    {
        playerController.InventoryInteraction = true;
        ShowTooltip(itemObject);
    }

    public void OnTooltipExit()
    {
        playerController.InventoryInteraction = false;
        HideTooltip();
    }
}
