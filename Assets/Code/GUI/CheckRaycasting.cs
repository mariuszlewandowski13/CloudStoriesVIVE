using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRaycasting : MonoBehaviour {

    public Transform raycastingGameObject;

    public bool isRaycasting;

    private RaycastHit hit;

    private Vector3 raycastDirection;

    private void Start()
    {
        if (GetComponent<SceneObjectInfo>() != null && GetComponent<SceneObjectInfo>().obj.type == ObjectsTypes.shapeObject)
        {
            raycastDirection = -transform.up;
        }
        else {
            raycastDirection = transform.forward;
        }
    }

    void Update()
    {
        if (raycastingGameObject != null)
        {
            Ray ray = new Ray(transform.position, raycastDirection);

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
