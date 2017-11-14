using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PointableGUIButton))]
public class ObjectRaycastScalingScript : MonoBehaviour, IClickable
{

    public bool click;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        clickingObject.GetComponent<RaycasterScalingScript>().StartScaling(gameObject, pos, clickingObject);
    }


    private void Update()
    {
        if (click)
        {
            click = false;
            Clicked(new Vector3(), gameObject);
        }
    }
}
