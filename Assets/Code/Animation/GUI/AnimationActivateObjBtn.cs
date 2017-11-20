using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class AnimationActivateObjBtn : GUIButton, IClickable
{

    public AnimationActivateObjBtn objectToActivate;

    public bool click;
    public bool activating;


    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive)
        {
            DeactivateObject();
            objectToActivate.ActivateObject();
            UpdateObjectPresention();
        }
    }

    private void UpdateObjectPresention()
    {
        transform.parent.GetComponent<AnimationObjectActivationManager>().ActivateDeactivateObject(activating);
    }

    
}
