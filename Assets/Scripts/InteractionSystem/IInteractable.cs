using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public ItemObject Item { get; }
    public bool Interact(Interactor interactor);
    public int GetAmount();

}
