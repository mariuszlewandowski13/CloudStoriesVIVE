using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class ChangeSceneBtn : MonoBehaviour, IClickable {

    private int sceneToLoadID = 0;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        GameObject.Find("LoadScene").GetComponent<CreateProjectScript>().LoadNewScene(sceneToLoadID);
    }

    public void SetSceneID(int newId)
    {
        sceneToLoadID = newId;
        ChangeSceneNumberInfoShowing();
    }

    private void ChangeSceneNumberInfoShowing()
    {
        transform.Find("Text").GetComponent<TextMesh>().text = sceneToLoadID.ToString();
    }
}
