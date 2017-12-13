using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSceneActionButton : ActionButton, IClickable {

    public ObjectActionType actionType;

    private void OnEnable()
    {
        Start();
    }

    private void Start()
    {
        ActivateObject();
        if (guiManager != null && guiManager.actualSelectedGameObject.GetComponent<ObjectActionsScript>().CheckActionExists(actionType))
        {
           string addData =  guiManager.actualSelectedGameObject.GetComponent<ObjectActionsScript>().CheckAndReturnActionTypeExists(actionType).additionalData;
            AddAction(addData);
        }
        else
        {
            RemoveAction();
        }
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive && guiManager != null && guiManager.actualSelectedGameObject != null)
        {
            guiManager.ActivateActionsChangeScene();
        }
    }

  
}
