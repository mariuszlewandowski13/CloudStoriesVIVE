﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class Object3DSpawningButton : MonoBehaviour, IClickable {

    public Object3DInfo object3DInfo;

    public GameObject objectToSpawn;

    public string basePath;

    public bool click;

    public virtual void Clicked(Vector3 pos, GameObject clickingObject)
    {
        clickingObject.GetComponent<RaycastObjectSpawner>().StartSpawning3DObject(pos, transform.lossyScale, transform.rotation.eulerAngles, object3DInfo,  basePath);
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
