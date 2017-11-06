using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastObjectSpawner : RaycastBase {

    private GameObject enviroment;

   

    private ControllerScript controller;

    private GameObject spawnedObject;

    private bool spawning;

    private ObjectsTypes spawnedObjectType;

    

    private RaycastHit hit;

    private GameObject rotationCenter;

    void Start()
    {
        controller = GetComponent<ControllerScript>();
        //trackedObj = transform.parent.GetComponent<SteamVR_TrackedObject>();
        
        enviroment = GameObject.Find("SCENE");
    }

    public void StartSpawning(GameObject objectToSpawn, Vector3 pos, Vector3 scale, Vector3 rotation,  ObjectsTypes objType, string path, string type3 = "",  string additionalData = "")
    {

        spawning = true;
        spawnedObject = enviroment.GetComponent<EnviromentMAnager>().SpawnObject(objectToSpawn, pos,scale, objType, Quaternion.Euler(rotation));
        //  

       
        if (objType == ObjectsTypes.shapeObject)
        {
            spawnedObject.transform.Rotate(90.0f, 0.0f, 0.0f);
        }
        spawnedObjectType = objType;
        //spawnedObject.transform.rotation = Quaternion.Euler(rot);

        //  spawnedObject.transform.Rotate(90.0f, 0.0f, 0.0f);
        spawnedObject.GetComponent<ObjectDatabaseUpdater>().SetTypesAndCreate(objType, path, type3, additionalData);

        GetComponent<ControllerRaycastScript>().isActive = false;

        rotationCenter = new GameObject();
        rotationCenter.transform.position = transform.position;
        rotationCenter.transform.rotation = transform.rotation;
        // rotationCenter.transform.LookAt(gameObject.transform);


        spawnedObject.transform.parent = rotationCenter.transform;

    }

    private void Update()
    {
        if (spawning)
        {

            spawnedObject.transform.parent = null;

            spawnedObject.SetActive(false);

            Ray ray = new Ray(transform.position, transform.forward);

            Physics.Raycast(ray, out hit, 3);
            if (hit.transform != null)
            {
                hitPoint = hit.point;
            }
            else
            {
                hitPoint = transform.forward * 3 + transform.position;
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
        spawnedObject.transform.parent = null;
        Destroy(rotationCenter);
        
        spawning = false;
        GetComponent<ControllerRaycastScript>().isActive = true;
        CursorOff();
        spawnedObject.AddComponent<ObjectInteractionScript>();
        spawnedObject.AddComponent<ImageMoveScript>();
        spawnedObject.AddComponent<SelectingObjectsScript>();
    }

   

}
