using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class SavelayoutScript : MonoBehaviour, IClickable{


    public GameObject enviroment;
    public bool click;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (enviroment != null)
        {
            enviroment.GetComponent<EnviromentMAnager>().SaveLayout();
        }

    }


    private void Update()
    {
        if (click)
        {
            click = false;
            Clicked(new Vector3(), gameObject);
        }
    }
}
