using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentShwoCheckbox : GUICheckbox, IClickable
{
    public GUIManager guiManager;

    private bool initialized;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (!initialized)
        {
            SetCheckboxActive(true);
            initialized = true;
        }
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (guiManager != null)
        {
            isActive = !isActive;
            guiManager.ShowHideEnviroment(isActive);
        }
    }

    public void SetCheckboxActive(bool act)
    {
        SetIsActive(act);
    }
}
