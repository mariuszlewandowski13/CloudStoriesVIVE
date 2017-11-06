using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Object3D : SceneObject
{


    public Object3D(string path, int ID, ObjectsTypes type, Vector3 pos, Vector3 rot, Vector3 scale) : base(ID, pos, rot, scale, type, path)
    {

    }
}

