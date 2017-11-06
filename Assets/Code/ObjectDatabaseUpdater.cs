using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(SceneObjectInfo))]
public class ObjectDatabaseUpdater : MonoBehaviour {

    public DatabaseController database;

    public SceneObjectInfo sceneObjInfo;

    public bool creatingObject;

    public string objectType3;

    public string objectType2;

    public ObjectsTypes objectType;

    public Texture2D tex;

    private byte[] textureBytes;

    private string extension;

    private bool existing;

    void Start () {
        sceneObjInfo = GetComponent<SceneObjectInfo>();
        existing = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!creatingObject && sceneObjInfo.obj != null && database != null && existing) 
        {
            if (transform.position.x != sceneObjInfo.obj.lastSavedPosition.x || transform.position.y != sceneObjInfo.obj.lastSavedPosition.y || transform.position.z != sceneObjInfo.obj.lastSavedPosition.z || transform.rotation.eulerAngles.x != sceneObjInfo.obj.lastSavedRotation.x || transform.rotation.eulerAngles.y != sceneObjInfo.obj.lastSavedRotation.y || transform.rotation.eulerAngles.z != sceneObjInfo.obj.lastSavedRotation.z || transform.lossyScale.x != sceneObjInfo.obj.lastSavedScale.x || transform.lossyScale.y != sceneObjInfo.obj.lastSavedScale.y || transform.lossyScale.z != sceneObjInfo.obj.lastSavedScale.z)
            {
                UpdateObjectPosRot();
            }
        }
	}

    public void SetSceneObject(SceneObject newSceneObj)
    {
        sceneObjInfo.obj = newSceneObj;
        creatingObject = false;
    }

    public void UpdateObjectPosRot()
    {
        if (database != null)
        {
            sceneObjInfo.obj.lastSavedPosition = transform.position;
            sceneObjInfo.obj.lastSavedRotation = transform.rotation.eulerAngles;
            sceneObjInfo.obj.lastSavedScale = transform.lossyScale;

            database.UpdateObject(sceneObjInfo.obj.lastSavedPosition, sceneObjInfo.obj.lastSavedRotation, sceneObjInfo.obj.lastSavedScale, sceneObjInfo.obj.ID);
        }
    }

    public void CreateSceneObject()
    {
        if (GameObject.Find("LoadScene") != null)
        {
            creatingObject = true;
            database = GameObject.Find("LoadScene").GetComponent<DatabaseController>();
            database.SaveObject(objectType, objectType2, transform.position, transform.rotation.eulerAngles, transform.lossyScale, SetSceneObject, textureBytes , extension);
        }
    }

    public void DestroySceneObject()
    {
        if (database != null)
        {
            existing = false; 
            database.DeleteObject(sceneObjInfo.obj.ID);
        }
    }

    public void SetTypesAndCreate(ObjectsTypes type, string type2, string type3, string additionalData)
    {
        objectType = type;
        objectType2 = type2;
        objectType3 = type3;
        if (type == ObjectsTypes.shapeObject)
        {
            Load2DShape();
            extension = additionalData;
        }
        CreateSceneObject(); 
    }


    public void Load2DShape()
    {
        if (objectType2 != "")
        {
            textureBytes = File.ReadAllBytes(objectType3 + objectType2);
            tex = new Texture2D(2, 2);
            tex.LoadImage(textureBytes);
            GetComponent<Renderer>().material.mainTexture = tex;
        }
    }

}
