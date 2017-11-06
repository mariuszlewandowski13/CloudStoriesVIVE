using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRaycasting : MonoBehaviour {

    public Transform raycastingGameObject;

    public bool isRaycasting;


    private RaycastHit hit;

    void Update()
    {
        if (raycastingGameObject != null)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            Physics.Raycast(ray, out hit, 5);


            if (hit.transform != null && hit.transform == raycastingGameObject)
            {
                isRaycasting = true;
            }
            else {
                isRaycasting = false;
            }
        }
    }
}
