using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;

[RequireComponent(typeof(DatabaseController))]
public class CreateProjectScript : MonoBehaviour {

    public GameObject shapeObjectPrefab;
    public GameObject object3DPrefab;

    public GameObject spawnedObjectsParent;

    public bool updating;

    private EnviromentMAnager envirMan;

	void Start () {
        if (ApplicationStaticData.projectToLoad < 0 )
        {
            CreateProject();
        }
        else {
            ApplicationStaticData.actualProject = new ProjectObject(ApplicationStaticData.projectToLoad, "", ApplicationStaticData.owner.ToString(), "");
            if (GameObject.Find("GUI") != null)
            {
                GameObject.Find("GUI").GetComponent<GUIManager>().SetProjectID(ApplicationStaticData.projectToLoad);
            }
            envirMan = GameObject.Find("SCENE").GetComponent<EnviromentMAnager>();
            LoadProject();
        }
     
}

    public void CreateProject()
    {
        GetComponent<DatabaseController>().CreateProject(GetNewProjectInfo);
    }

    private void GetNewProjectInfo(ProjectObject proj)
    {
        ApplicationStaticData.actualProject = proj;
        if (GameObject.Find("GUI") != null)
        {
            GameObject.Find("GUI").GetComponent<GUIManager>().SetProjectID(proj.id);
        }
    }

    private void LoadProject()
    {
        updating = true;
        Download();
    }

    void Download()
    {
        // Create a request for the URL.   
        WebRequest request = WebRequest.Create(
          "http://vrowser.e-kei.pl/CloudStories/" + "GetProjectData.php?ID=" + ApplicationStaticData.actualProject.id.ToString());
        // If required by the server, set the credentials.  
        request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.  
        Stream dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.  
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.  
        string responseFromServer = reader.ReadToEnd();
        string[] res;
        string[] msg = responseFromServer.Split(new string[] { "@@@@@" }, StringSplitOptions.None);
        foreach (string row in msg)
        {
            res = row.Split(new string[] { "#####" }, StringSplitOptions.None);
            if (res.Length > 1)
            {
                ProcessLine(res);
            }
        }

        // Display the content.  
        Debug.Log(responseFromServer);
        // Clean up the streams and the response.  
        reader.Close();
        response.Close();

        updating = false;
    }

    private void ProcessLine(string[] line)
    {
        ObjectsTypes objType = (ObjectsTypes)Int32.Parse(line[1]);
        GameObject newObject = null;
        int objectNumber = Int32.Parse(line[0]);

        Vector3 pos = new Vector3();
        Vector3 rot = new Vector3();
        Vector3 size = new Vector3();

        if (objType == ObjectsTypes.object3D)
        {
                int number;

                 pos = new Vector3(float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
                 rot = new Vector3(float.Parse(line[6]), float.Parse(line[7]), float.Parse(line[8]));
                 size = new Vector3(float.Parse(line[9]), float.Parse(line[10]), float.Parse(line[11]));

                if (Int32.TryParse(line[2], out number))
                {
                    newObject = Instantiate(envirMan.objectsPrefabs[number], pos, Quaternion.Euler(rot));
                }
                else
                {
                    newObject = Load3DObject(line[2], pos, rot, objectNumber);
                }


            newObject.transform.parent = spawnedObjectsParent.transform;
                newObject.transform.localPosition = pos;
                newObject.transform.localScale = size;

            


            CreateScaleHandler(objType, newObject.transform.position, newObject.transform.rotation, newObject, false);

        }
        else if (objType == ObjectsTypes.shapeObject)
        {

                 pos = new Vector3(float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
                 rot = new Vector3(float.Parse(line[6]), float.Parse(line[7]), float.Parse(line[8]));
                 size = new Vector3(float.Parse(line[9]), float.Parse(line[10]), float.Parse(line[11]));

            int number;

            Vector3 additionalRotation = new Vector3();

            float sizeMultiplier = 1.0f;

            if (Int32.TryParse(line[2], out number))
            {
                newObject = Load2DObject(number, pos, rot);
                
                additionalRotation = new Vector3(90.0f, 0.0f, 0.0f);
                sizeMultiplier = 0.0f;
            }
            else
            {
                newObject = Instantiate(shapeObjectPrefab, pos, Quaternion.Euler(rot));
                newObject.AddComponent<ImageScript>();
                newObject.GetComponent<ImageScript>().SetImagePath(line[2]);
                
            }

            

            newObject.transform.parent = spawnedObjectsParent.transform;
            newObject.transform.localPosition = pos;
            newObject.transform.localScale = size;
            if (sizeMultiplier == 0.0f)
            {
                sizeMultiplier = Mathf.Max(newObject.transform.localScale.x, newObject.transform.localScale.y, newObject.transform.localScale.z)/4.0f;
            }
            

            CreateScaleHandler(objType, newObject.transform.position, newObject.transform.rotation, newObject, false, sizeMultiplier, additionalRotation);

        }
        if (newObject != null)
        {
            newObject.GetComponent<ObjectDatabaseUpdater>().SetSceneObject(new SceneObject(objectNumber, pos, rot, size, objType, line[2]));
            newObject.AddComponent<ObjectInteractionScript>();
            newObject.AddComponent<ImageMoveScript>();
            newObject.AddComponent<SelectingObjectsScript>();
            newObject.AddComponent<ObjectAnimationScript>();
        }

    }


    private GameObject Load3DObject(string path, Vector3 pos, Vector3 rot, int id)
    {
        GameObject newObject = Instantiate(object3DPrefab, pos, Quaternion.Euler(rot));
        newObject.AddComponent<Object3DScript>();
        newObject.GetComponent<Object3DScript>().LoadObject(path, "object3D.obj", "tex.png", id);
        return newObject;
    }

    private GameObject Load2DObject(int number, Vector3 pos, Vector3 rot)
    {
        GameObject newObject = Instantiate(envirMan.shapesPrefabs[number], pos, Quaternion.Euler(rot));
        return newObject;
    }

    private void CreateScaleHandler(ObjectsTypes objectType, Vector3 position, Quaternion rotation, GameObject newObject, bool selfRotation, float sizeMultiplier = 1.0f, Vector3 additionalRotation = new Vector3())
    {
        GameObject scaleHandler = null;

        if (selfRotation)
        {
            scaleHandler = Instantiate(envirMan.scaleHandlers[(int)objectType], position, envirMan.scaleHandlers[(int)objectType].transform.rotation);
        }
        else
        {
            scaleHandler = Instantiate(envirMan.scaleHandlers[(int)objectType], position, rotation);
        }

        scaleHandler.transform.Rotate(additionalRotation);
        scaleHandler.transform.localScale = scaleHandler.transform.localScale * sizeMultiplier;

        scaleHandler.name = "ScaleHandler";

        scaleHandler.transform.parent = newObject.transform;
    }

}
