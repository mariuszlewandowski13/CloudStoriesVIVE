using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeleteActionButton : ActionButton, IClickable {

    public ObjectActionType actionType;

    public GameObject buttonToRefresh;

    private void OnEnable()
    {
        Start();
    }

    private void Start()
    {
        if (guiManager != null && guiManager.actualSelectedGameObject.GetComponent<ObjectActionsScript>().CheckActionExists(actionType))
        {
            ActivateObject();
        }
        else {
            DeactivateObject();
        }
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && guiManager != null && guiManager.actualSelectedGameObject != null)
        {
            guiManager.actualSelectedGameObject.GetComponent<ObjectActionsScript>().RemoveAction(actionType);
            if (buttonToRefresh != null)
            {
                buttonToRefresh.GetComponent<ActionButton>().RemoveAction();
            }
            DeactivateObject();
        }
    }
}
