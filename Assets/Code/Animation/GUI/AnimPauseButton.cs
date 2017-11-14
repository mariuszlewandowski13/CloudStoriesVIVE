using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPauseButton : AnimGUIButton, IClickable {

    public AnimationManager animManager;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && animManager != null)
        {
            animManager.Pause();
        }
    }
}
