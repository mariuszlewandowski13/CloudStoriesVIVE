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

    private byte[] additionalTextureBytes;

    private byte[] textureBytes = null;

    private string extension;

    private bool existing;

    private void Awake()
    {

        sceneObjInfo = GetComponent<SceneObjectInfo>();
    }

    void Start () {
        
        existing = true;
        InvokeRepeating("UpdateInfo",5.0f, 5.0f);
    }

    // Update is called once per frame
    void UpdateInfo()
    {
        if (!creatingObject && sceneObjInfo.obj != null && database != null && existing && !AnimationManager.isWorking)
        {
            if (transform.localPosition.x != sceneObjInfo.obj.lastSavedPosition.x || transform.localPosition.y != sceneObjInfo.obj.lastSavedPosition.y || transform.localPosition.z != sceneObjInfo.obj.lastSavedPosition.z || transform.rotation.eulerAngles.x != sceneObjInfo.obj.lastSavedRotation.x || transform.rotation.eulerAngles.y != sceneObjInfo.obj.lastSavedRotation.y || transform.rotation.eulerAngles.z != sceneObjInfo.obj.lastSavedRotation.z || transform.localScale.x != sceneObjInfo.obj.lastSavedScale.x || transform.localScale.y != sceneObjInfo.obj.lastSavedScale.y || transform.localScale.z != sceneObjInfo.obj.lastSavedScale.z)
            {
                UpdateObjectPosRot();
            }
        }
    }

    public void SetSceneObject(SceneObject newSceneObj)
    {
        sceneObjInfo.obj = newSceneObj;
        database = GameObject.Find("LoadScene").GetComponent<DatabaseController>();
        creatingObject = false;
        existing = true;
    }

    public void UpdateObjectPosRot()
    {
        if (database != null)
        {
            sceneObjInfo.obj.lastSavedPosition = transform.localPosition;
            sceneObjInfo.obj.lastSavedRotation = transform.rotation.eulerAngles;
            sceneObjInfo.obj.lastSavedScale = transform.localScale;

            database.UpdateObject(sceneObjInfo.obj.lastSavedPosition, sceneObjInfo.obj.lastSavedRotation, sceneObjInfo.obj.lastSavedScale, sceneObjInfo.obj.ID);
        }
    }

    public void CreateSceneObject()
    {
        if (GameObject.Find("LoadScene") != null)
        {
            creatingObject = true;
            database = GameObject.Find("LoadScene").GetComponent<DatabaseController>();
            database.SaveObject(objectType, objectType2, transform.position, transform.rotation.eulerAngles, transform.lossyScale, SetSceneObject, textureBytes , extension, additionalTextureBytes);
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

    public void SetTypesAndCreate(ObjectsTypes type, string type2, string type3,string additionalData = "")
    {
        objectType = type;
        objectType2 = type2;
        objectType3 = type3;
        if (type == ObjectsTypes.shapeObject)
        {
            Load2DShape();
            extension = additionalData;
        }
        else if (type == ObjectsTypes.movie)
        {
            LoadMovie();
        }
        else if (type == ObjectsTypes.object3D)
        {
            Load3DObjectBytes();
            extension = additionalData;
        }
        CreateSceneObject(); 
    }

    public void Load3DObjectBytes()
    {
        int number;
        if (objectType2 != "" && !int.TryParse(objectType2, out number))
        {
            textureBytes = File.ReadAllBytes(objectType2);
        }

        if (objectType3 != "")
        {
            additionalTextureBytes = File.ReadAllBytes(objectType3);
        }
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

    public void LoadMovie()
    {
        if (objectType2 != "")
        {
            GetComponent<MediaPlayerCtrl>().m_strFileName = objectType3 + objectType2;
        }
    }

}
