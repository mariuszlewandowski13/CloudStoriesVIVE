using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class AnimPauseButton : GUIButton, IClickable {

    public AnimationManager animManager;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && animManager != null)
        {
            animManager.Pause();
        }
    }
}
