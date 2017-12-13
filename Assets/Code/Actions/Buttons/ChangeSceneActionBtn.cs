using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class ChangeSceneActionBtn : MonoBehaviour, IClickable
{

    private int sceneToLoadID = 0;

    private GUIManager manager;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (manager != null)
        {
            manager.actualSelectedGameObject.GetComponent<ObjectActionsScript>().AddAction(new ObjectAction(true, ObjectActionType.changeScene, sceneToLoadID.ToString()));
            manager.CancelActivateActions();
        }
    }

    public void SetSceneID(int newId, GUIManager man)
    {
        sceneToLoadID = newId;
        manager = man;
        ChangeSceneNumberInfoShowing();
    }

    private void ChangeSceneNumberInfoShowing()
    {
        transform.Find("Text").GetComponent<TextMesh>().text = sceneToLoadID.ToString();
    }
}
