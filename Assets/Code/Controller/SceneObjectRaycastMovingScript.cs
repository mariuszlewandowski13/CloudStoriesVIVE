using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PointableGUIButton))]
public class SceneObjectRaycastMovingScript : MonoBehaviour, IClickable {


    public bool click;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        clickingObject.GetComponent<RaycastMoveScript>().StartMoving(gameObject, transform.position);

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
