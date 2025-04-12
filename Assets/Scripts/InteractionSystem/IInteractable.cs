using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public ItemObject Item { get; }
    public GameObject Spawner { get; }
    public bool Interact(Interactor interactor);
    public int GetAmount();

}
