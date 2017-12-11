using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class VideoCreateBtn : GUIButton, IClickable
{
    public VideoFromURLManager manager;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && manager)
        {
            manager.CreateVideo();
        }
    }
}
