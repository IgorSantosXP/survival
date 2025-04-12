using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToRespawn;
    [SerializeField] private int timeToRespawn = 5;

    void Start() {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn() {
        yield return new WaitForSeconds(timeToRespawn);
        Instantiate(objectToRespawn, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
