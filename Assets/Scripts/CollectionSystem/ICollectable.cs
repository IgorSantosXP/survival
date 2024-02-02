using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Tree,
    Rock
}

public interface ICollectable
{
    public GameObject ObjectToSpawn { get; }
    public string Name { get; }
    public ItemObject Item { get; }
    public int ResourceAmount { get; }
    public int ResourceAmountByHit { get; }
    public int HitsToDestroy { get; }
    public CollectableType type { get; }
    public void Hit();
    public int GetAmount();
}
