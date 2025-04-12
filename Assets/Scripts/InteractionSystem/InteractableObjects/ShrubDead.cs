using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubDead : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Animator animator;
    [SerializeField] private ItemObject item;
    [SerializeField] private GameObject spawner;
    public string InteractionPrompt => _prompt;
    public ItemObject Item => item;
    public GameObject Spawner => spawner;
    private bool isInteracting = false;
    private int minAmountItem = 5;
    private int maxAmountItem = 11;

    public bool Interact(Interactor interactor)
    {
        if (!isInteracting)
        {
            animator.SetTrigger("ShrubDeadInteract");
            isInteracting = true;
        }
        
        return true;
    }

    public int GetAmount()
    {
        return Random.Range(minAmountItem, maxAmountItem);
    }
}
