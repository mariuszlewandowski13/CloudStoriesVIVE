using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PointableGUIButton))]
public class CancelChoosingScene : GUIButton, IClickable
{
    public GUIManager manager;

    private void Start()
    {
        ActivateObject();
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && manager != null)
        {
            manager.CancelActivateActions();
        }

    }

    public void SetGuiManager(GUIManager man)
    {
        manager = man;
        transform.Find("Text").GetComponent<TextMesh>().text = "Cancel";
    }
}
