using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;
    public QuickAccess QuickAccessContainer;
    [SerializeField] private LogController log;

    public void AddItem(Item _item, int _amount)
    {
        if (_item.recipe.Length > 0)
        {
            SetEquippableItemEmptySlot(_item, _amount);
            return;
        }

        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }

        for (int i = 0; i < QuickAccessContainer.Items.Length; i++)
        {
            if (QuickAccessContainer.Items[i].ID == _item.Id)
            {
                QuickAccessContainer.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }

    public void RemoveItemAmount(Item _item, int _amount)
    {
        if (_item.recipe.Length > 0)
        {
            return;
        }

        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].RemoveAmount(_amount);
                return;
            }
        }

        for (int i = 0; i < QuickAccessContainer.Items.Length; i++)
        {
            if (QuickAccessContainer.Items[i].ID == _item.Id)
            {
                QuickAccessContainer.Items[i].RemoveAmount(_amount);
                return;
            }
        }
    }

    private InventorySlot SetEquippableItemEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < QuickAccessContainer.Items.Length; i++)
        {
            if (QuickAccessContainer.Items[i].ID <= -1)
            {
                QuickAccessContainer.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return QuickAccessContainer.Items[i];
            }
        }

        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }

        return null;
    }

    private InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }

        for (int i = 0; i < QuickAccessContainer.Items.Length; i++)
        {
            if (QuickAccessContainer.Items[i].ID <= -1)
            {
                QuickAccessContainer.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return QuickAccessContainer.Items[i];
            }
        }

        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
                return;
            }
        }

        for (int i = 0; i < QuickAccessContainer.Items.Length; i++)
        {
            if (QuickAccessContainer.Items[i].item == _item)
            {
                QuickAccessContainer.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    public int GetItemAmount(int itemId)
    {
        int itemAmount = 0;
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == itemId)
            {
                itemAmount += Container.Items[i].amount;
            }
        }

        for (int i = 0; i < QuickAccessContainer.Items.Length; i++)
        {
            if (QuickAccessContainer.Items[i].ID == itemId)
            {
                itemAmount += QuickAccessContainer.Items[i].amount;
            }
        }

        return itemAmount;
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class QuickAccess
{
    public InventorySlot[] Items = new InventorySlot[6];
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[35];
}

[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public Item item;
    public int amount;
    public GameObject slotPrefab;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        if (amount == value)
        {
            UpdateSlot(-1, null, 0);
            return;
        }
        amount -= value;
    }
}

