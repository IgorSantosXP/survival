using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyObject : MonoBehaviour
{
    [SerializeField] private int secondsToDestroy;
    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}
