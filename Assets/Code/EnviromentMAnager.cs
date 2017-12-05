using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class EnviromentMAnager : MonoBehaviour {

    public GameObject object3DPrefab;

    public GameObject[] scaleHandlers;
    public GameObject[] objectsPrefabs;

    public GameObject[] shapesPrefabs;

    public GameObject table;

    private GameObject layoutObject;
    private GameObject sceneObjects;



    private void Start()
    {
        layoutObject = transform.Find("Layout").gameObject;
        sceneObjects = transform.Find("SceneObjects").gameObject;
    }

    public GameObject SpawnObject(GameObject objToSpawn, Vector3 position, Vector3 scale, ObjectsTypes objectType, Quaternion rotation = new Quaternion(), bool selfRotation = false, Mesh mesh = null)
    {

        GameObject newObject = null;

        if (objectType == ObjectsTypes.object3D && objToSpawn == null)
        {
            newObject = Instantiate(object3DPrefab, position, rotation);
        }
        else {
            newObject = Instantiate(objToSpawn, position, rotation);
        }

        newObject.transform.localScale = scale * 2.0f;
        newObject.transform.parent = sceneObjects.transform;

        GameObject scaleHandler = null; 

        if (selfRotation)
        {
             scaleHandler = Instantiate(scaleHandlers[(int)objectType], position, scaleHandlers[(int)objectType].transform.rotation);
        }
        else {
             scaleHandler = Instantiate(scaleHandlers[(int)objectType], position, rotation);
        }

        //if (mesh != null)
        //{

        //}
        

        scaleHandler.name = "ScaleHandler";

        scaleHandler.transform.parent = newObject.transform;

        return newObject;
    }



    public void LoadLayout(GameObject newLayout, Vector3 pos, int layoutNumber, Quaternion rotation = new Quaternion())
    {
        if (layoutObject.transform.childCount > 0)
        {
            ClearLayout();
        }

        GameObject newLay = Instantiate(newLayout, layoutObject.transform.position, rotation);
        newLay.transform.parent = layoutObject.transform;
        newLay.transform.localPosition = pos;
    }

    private void ClearLayout()
    {
        if (layoutObject != null)
        {
            foreach (Transform child in layoutObject.transform)
            {
                Destroy(child.gameObject);
            }
        }

    }

    public void SaveLayout()
    {
        GameObject.Find("LoadScene").GetComponent<DatabaseController>().SaveLayout(SaveLayoutsElements);
    }

    private void SaveLayoutsElements(int layoutID)
    {
        foreach (Transform child in sceneObjects.transform)
        {
            SceneObject sceneObj = child.GetComponent<SceneObjectInfo>().obj;
            GameObject.Find("LoadScene").GetComponent<DatabaseController>().SaveLayoutObject(sceneObj.type, sceneObj.path, child.position, child.rotation.eulerAngles, child.transform.lossyScale, layoutID);
        }
    }

    public void ShowHideEnviroment(bool active)
    {
        if (table != null)
        {
            table.SetActive(active);
        }
        
    }

}
