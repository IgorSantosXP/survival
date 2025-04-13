using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footstepClips;
    [SerializeField] private AudioClip punchClip;
    [SerializeField] private AudioClip toolMoveClip;

    private void PlayFootstep() {
        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Count)];

        SoundManager.Instance.PlayPlayerSound(clip);
    }

    private void PlayPunch() {
        SoundManager.Instance.PlayPlayerSound(punchClip);
    }

    private void PlayToolMove() {
        SoundManager.Instance.PlayPlayerSound(toolMoveClip);
    }
}
