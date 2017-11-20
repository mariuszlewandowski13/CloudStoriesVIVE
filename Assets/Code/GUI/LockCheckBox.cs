using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class LockCheckBox : GUICheckbox, IClickable {


    public GUIManager guiManager;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (guiManager != null)
        {
            ChangeActive();
            guiManager.ChangeActualObjectLock(isActive);
        }
        
           
    }

    public void SetCheckboxActive(bool act)
    {
        SetIsActive(act);
    }

}
