using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class Object3DSpawningButton : MonoBehaviour, IClickable {

    public GameObject objectToSpawn;

    public string basePath;

    public bool click;

    public virtual void Clicked(Vector3 pos, GameObject clickingObject)
    {
        clickingObject.GetComponent<RaycastObjectSpawner>().StartSpawning(objectToSpawn, pos, transform.lossyScale, transform.rotation.eulerAngles, ObjectsTypes.object3D, basePath);

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
