using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastObjectSpawner : RaycastBase {

    private GameObject enviroment;

    private ControllerScript controller;

    private GameObject spawnedObject;

    private bool spawning;

    private ObjectsTypes spawnedObjectType;

    private float raycasterLength = 1.0f;

    private RaycastHit hit;

    private GameObject rotationCenter;

    private Transform spawningObjectPrevTransform;

    void Start()
    {
        controller = GetComponent<ControllerScript>();
        //trackedObj = transform.parent.GetComponent<SteamVR_TrackedObject>();
        
        enviroment = GameObject.Find("SCENE");
    }

    public void StartSpawning(GameObject objectToSpawn, Vector3 pos, Vector3 scale, Vector3 rotation,  ObjectsTypes objType, string path, string type3 = "",  string additionalData = "", bool selfRot = false)
    {

        spawning = true;
        raycasterLength = Vector3.Distance(pos, transform.position);
        spawnedObject = enviroment.GetComponent<EnviromentMAnager>().SpawnObject(objectToSpawn, pos,scale, objType, Quaternion.Euler(rotation), selfRot);

        spawnedObjectType = objType;
        //spawnedObject.transform.rotation = Quaternion.Euler(rot);

        //  spawnedObject.transform.Rotate(90.0f, 0.0f, 0.0f);
        spawnedObject.GetComponent<ObjectDatabaseUpdater>().SetTypesAndCreate(objType, path, type3, additionalData);

        GetComponent<ControllerRaycastScript>().isActive = false;

        rotationCenter = new GameObject();
        rotationCenter.transform.position = transform.position;
        rotationCenter.transform.rotation = transform.rotation;
        rotationCenter.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        spawningObjectPrevTransform = spawnedObject.transform.parent;

        spawnedObject.transform.parent = rotationCenter.transform;

    }

    public void StartSpawning3DObject(Vector3 pos, Vector3 scale, Vector3 rotation, Object3DInfo objInfo, string path, string type3 = "")
    {

        spawning = true;
        raycasterLength = Vector3.Distance(pos, transform.position);
        spawnedObject = enviroment.GetComponent<EnviromentMAnager>().SpawnObject(null, pos, scale, ObjectsTypes.object3D, Quaternion.Euler(rotation));
        spawnedObject.GetComponent<MeshFilter>().mesh = objInfo.mesh;

        string exten = "";

        if(objInfo.texturesInfos.Count > 0)
        {
            spawnedObject.GetComponent<Renderer>().material.mainTexture = objInfo.texturesInfos[0].tex;
            type3 = objInfo.texturesInfos[0].path + "/" + objInfo.texturesInfos[0].name;
            exten = objInfo.texturesInfos[0].ext;
        }

        spawnedObject.AddComponent<BoxCollider>();
        

        spawnedObjectType = ObjectsTypes.object3D;
        //spawnedObject.transform.rotation = Quaternion.Euler(rot);

        //  spawnedObject.transform.Rotate(90.0f, 0.0f, 0.0f);



        spawnedObject.GetComponent<ObjectDatabaseUpdater>().SetTypesAndCreate(ObjectsTypes.object3D, path,type3 , exten);

        GetComponent<ControllerRaycastScript>().isActive = false;

        rotationCenter = new GameObject();
        rotationCenter.transform.position = transform.position;
        rotationCenter.transform.rotation = transform.rotation;
        rotationCenter.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        spawningObjectPrevTransform = spawnedObject.transform.parent;
        spawnedObject.transform.parent = rotationCenter.transform;

    }

    public void StartSpawning3DObject(Vector3 pos, Vector3 scale, Vector3 rotation,  string path, string type3 = "")
    {

        spawning = true;
        raycasterLength = Vector3.Distance(pos, transform.position);


        GameObject prefab = null;

        int number;
        if (int.TryParse(path, out number))
        {
            prefab = GameObject.Find("SCENE").GetComponent<EnviromentMAnager>().objectsPrefabs[number];
        }


        spawnedObject = enviroment.GetComponent<EnviromentMAnager>().SpawnObject(prefab, pos, scale, ObjectsTypes.object3D, Quaternion.Euler(rotation));


        spawnedObjectType = ObjectsTypes.object3D;

        spawnedObject.GetComponent<ObjectDatabaseUpdater>().SetTypesAndCreate(ObjectsTypes.object3D, path, type3);

        GetComponent<ControllerRaycastScript>().isActive = false;

        rotationCenter = new GameObject();
        rotationCenter.transform.position = transform.position;
        rotationCenter.transform.rotation = transform.rotation;
        rotationCenter.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        spawningObjectPrevTransform = spawnedObject.transform.parent;
        spawnedObject.transform.parent = rotationCenter.transform;

    }

    private void Update()
    {
        if (spawning)
        {

            spawnedObject.transform.parent = null;

            spawnedObject.SetActive(false);

            Ray ray = new Ray(transform.position, transform.forward);

            Physics.Raycast(ray, out hit, raycasterLength);
            if (hit.transform != null && (hit.transform.tag == "RaycastSpecialColliders" || hit.transform.tag == "ScrollingPanel"))
            {
                hitPoint = hit.point;
            }
            else
            {
                hitPoint = transform.forward * raycasterLength + transform.position;
            }
            CursorOn();
            spawnedObject.transform.position = hitPoint;
            rotationCenter.transform.position = transform.position;
            spawnedObject.transform.parent = rotationCenter.transform;

            
            rotationCenter.transform.rotation = transform.rotation;
            spawnedObject.SetActive(true);

            //spawnedObject.transform.LookAt(gameObject.transform);
            //spawnedObject.transform.Rotate(90.0f, 0.0f, 0.0f);

            if (controller.triggerUp)
            {
                StopSpawning();
            }
        }
    }


    public void StopSpawning()
    {
        spawnedObject.transform.parent = spawningObjectPrevTransform;
        Destroy(rotationCenter);
        
        spawning = false;
        GetComponent<ControllerRaycastScript>().isActive = true;
        CursorOff();
        spawnedObject.AddComponent<ObjectInteractionScript>();
        spawnedObject.AddComponent<ImageMoveScript>();
        spawnedObject.AddComponent<SelectingObjectsScript>();
        spawnedObject.AddComponent<ObjectAnimationScript>();
        spawnedObject.AddComponent<ObjectActionsScript>();
    }

   

}
