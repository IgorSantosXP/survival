using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Resource,
    Tool,
    Default
}

//public enum Attributes
//{
//    Agility,
//    Intellect,
//    Stamina,
//    Strength
//}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public string Name;
    public int Level;
    public int CraftTime;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string Description;
    public ItemRecipe[] recipe;
    public GameObject itemPrefab;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemRecipe[] recipe;
    public GameObject itemRecipe;

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        recipe = new ItemRecipe[item.recipe.Length];
        itemRecipe = item.itemPrefab;

        //for (int i = 0; i < recipe.Length; i++)
        //{
        //    recipe[i] = new recipe(item.buffs[i].min, item.buffs[i].max)
        //    {
        //        attribute = item.buffs[i].attribute
        //    };

        //}
    }
}

[System.Serializable]
public class ItemRecipe
{
    public ResourceObject resource;
    public int value;
}