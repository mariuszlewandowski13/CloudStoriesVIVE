using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class ObjectDeleteButton : GUIButton, IClickable
{

    public GUIManager guiManager;

    public bool click;


    private void Start()
    {
        ActivateObject();   
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive)
        {
            guiManager.DeleteActualSelectedGameObject();
        }
    }

 
}
