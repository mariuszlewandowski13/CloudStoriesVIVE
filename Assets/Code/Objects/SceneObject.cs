using UnityEngine;
using System;

public enum ObjectsTypes
{
    shapeObject,
    object3D,
    movie,
    gif,
    audio,
    layout
}

[Serializable]
public class SceneObject   {
    public int ID;
    public Vector3 lastSavedPosition;
    public Vector3 lastSavedRotation;
    public Vector3 lastSavedScale;
    public ObjectsTypes type;
    public string path;

    protected SceneObject(int id, Vector3 pos, Vector3 rot, Vector3 scale, ObjectsTypes type, string path)
    {
        ID = id;
        lastSavedPosition = pos;
        lastSavedRotation = rot;
        lastSavedScale = scale;
        this.type = type;
        this.path = path;
    }

	
}
