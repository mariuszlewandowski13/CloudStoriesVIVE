using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class AddURLBtn :  GUIButton, IClickable
{

    public AddingURLManager manager;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && manager)
        {
            manager.SaveURL();
        }
    }
}
