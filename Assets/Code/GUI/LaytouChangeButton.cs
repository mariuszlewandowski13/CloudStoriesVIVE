using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class LaytouChangeButton : MonoBehaviour, IClickable {

    public GameObject workingEnviroment;
    public GameObject layoutToLoad;
    public Vector3 position;
    public int layoutNumber;

    public bool click;

    void Loadlayout()
    {
        workingEnviroment.GetComponent<EnviromentMAnager>().LoadLayout(layoutToLoad, position, layoutNumber, layoutToLoad.transform.rotation);
    }

   

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        Loadlayout();

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
