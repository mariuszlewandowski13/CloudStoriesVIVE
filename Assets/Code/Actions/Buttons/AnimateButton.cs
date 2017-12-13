using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PointableGUIButton))]
public class AnimateButton : ActionButton, IClickable
{
    public ObjectActionType actionType;


    private void OnEnable()
    {
        Start();
    }

    private void Start()
    {
        DeactivateObject();
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
      
    }

}
