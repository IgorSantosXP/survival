using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObject : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private string _name;
    [SerializeField] private ItemObject _item;
    [SerializeField] private int _resourceAmount;
    [SerializeField] private int _resourceAmountByHit;
    [SerializeField] private int _hitsToDestroy;
    [SerializeField] private CollectableType _type;
    [SerializeField] private GameObject spawner;
    private Animator animator;
    private SoundEffect soundEffect = SoundEffect.HitRock;

    public GameObject ObjectToSpawn => _objectToSpawn;
    public string Name => _name;
    public ItemObject Item => _item;
    public int ResourceAmount => _resourceAmount;
    public int ResourceAmountByHit => _resourceAmountByHit;
    public int HitsToDestroy => _hitsToDestroy;
    public CollectableType type => _type;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void PlaySound() {
        AudioClip audioClip = Resources.Load<AudioClip>($"Sounds/SFX/{soundEffect}");
        SoundManager.Instance.PlaySFX(audioClip);
    }

    public void Hit()
    {
        PlaySound();
        animator.SetTrigger("Hit");
        _hitsToDestroy--;
        _resourceAmount -= _resourceAmountByHit;
        if (_hitsToDestroy <= 0)
        {
            Instantiate(spawner, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public int GetAmount()
    {
        int resourceAmount = _resourceAmountByHit;
        if (_hitsToDestroy <= 1)
        {
            resourceAmount = _resourceAmount;
        }
        return resourceAmount;
    }
}
