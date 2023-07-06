using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TargetLookController : MonoBehaviour
{
    private Vector3 lookPos;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = new Vector3(hit.point.x, 0f, hit.point.z);
        }

        transform.position = lookPos;
        //Vector3 lookDir = lookPos - transform.position;
        //lookDir.y = 0;
        //transform.LookAt(transform.position + lookDir, Vector3.up);
    }
}
