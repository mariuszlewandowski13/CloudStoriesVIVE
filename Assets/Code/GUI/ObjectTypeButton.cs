using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PointableGUIButton))]
public class ObjectTypeButton : MonoBehaviour, IClickable {

    public GameObject objectToActivate;
    public static ObjectTypeButton actualActiveObject;

    public bool click;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (ControlObjects.scrollingGameObject == null)
        {
            if (objectToActivate != null)
            {
                if (actualActiveObject != null)
                {
                    actualActiveObject.objectToActivate.SetActive(false);
                }
                actualActiveObject = this;
                objectToActivate.SetActive(true);
            }
            ControlObjects.scroll = objectToActivate;
        }
       
        
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
