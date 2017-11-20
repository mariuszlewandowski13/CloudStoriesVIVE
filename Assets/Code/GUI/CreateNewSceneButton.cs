using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PointableGUIButton))]
public class CreateNewSceneButton : GUIButton, IClickable {

    private void Start()
    {
        isActive = true;
    }

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (isActive)
        {
            GameObject.Find("LoadScene").GetComponent<CreateProjectScript>().CreateNewScene(this);
            DeactivateObject();
        }
        
    }
}
