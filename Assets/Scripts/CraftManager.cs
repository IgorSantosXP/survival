using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    [SerializeField] private InventoryController inventory;
    [SerializeField] private GameObject warningMessage;
    [SerializeField] private GameObject warningMessageSpawner;
    [SerializeField] private GameObject craftSlider;

    public void CheckResources(GameObject itemGameObject)
    {
        ItemObject item = itemGameObject.GetComponent<CraftableItem>().Item;

        bool canCraftItem = true;
        for (int i = 0; i < item.recipe.Length; i++)
        {
            int itemQty = 0;
            for (int j = 0; j < inventory.Container.Items.Length; j++)
            {
                if (inventory.Container.Items[j].ID == item.recipe[i].resource.Id)
                {
                    itemQty += inventory.Container.Items[j].amount;
                }
            }

            if (itemQty < item.recipe[i].value)
            {

                canCraftItem = false;
            }
        }
        if (canCraftItem)
        {
            RemoveResources(item);
            CraftItem(itemGameObject, item);
        }
        else
        {
            GameObject warningMessageObj = Instantiate(warningMessage, warningMessageSpawner.transform);
            TextMeshProUGUI warningMessageText = warningMessageObj.GetComponentInChildren<TextMeshProUGUI>();
            warningMessageText.text = "Not enough resources!";
        }
    }

    private void RemoveResources(ItemObject item)
    {
        for (int i = 0; i < item.recipe.Length; i++)
        {
            inventory.RemoveItemAmount(new Item(item.recipe[i].resource), item.recipe[i].value);
        }
    }

    private void CraftItem(GameObject itemGameObject, ItemObject item)
    {
        GameObject sliderGameObject = Instantiate(craftSlider, itemGameObject.transform);
        CraftTimer craftTimer = sliderGameObject.GetComponent<CraftTimer>();
        craftTimer.maxValue = item.CraftTime;
        CoroutineManager.Instance.StartCoroutineUnstoppable(WaitAndCreateItem(itemGameObject, item));
    }

    IEnumerator WaitAndCreateItem(GameObject itemGameObject, ItemObject item)
    {
        Button itemButton = itemGameObject.GetComponent<Button>();
        itemButton.interactable = false;
        yield return new WaitForSeconds(item.CraftTime);
        itemButton.interactable = true;
        inventory.AddItem(new Item(item), 1);
    }
}
