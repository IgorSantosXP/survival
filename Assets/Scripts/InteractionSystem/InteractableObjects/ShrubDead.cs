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
    private SoundEffect soundEffect = SoundEffect.HarvestShrubDead;

    private void PlaySound() {
        AudioClip audioClip = Resources.Load<AudioClip>($"Sounds/SFX/{soundEffect}");
        SoundManager.Instance.PlaySFX(audioClip);
    }

    public bool Interact(Interactor interactor)
    {
        if (!isInteracting)
        {
            PlaySound();
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
