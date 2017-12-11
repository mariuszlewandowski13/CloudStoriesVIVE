using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class VideoEnterURLBtn : GUIButton, IClickable
{

    public VideoFromURLManager manager;

    private void Start()
    {
        isActive = true;
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && manager)
        {
            manager.EnterText();
        }
    }
}
