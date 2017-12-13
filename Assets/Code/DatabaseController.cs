﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpdateActionsObject
{
    public int objeID;
    public string actions;
    public string filename;
    public byte[] bytes;

    public UpdateActionsObject(int id, string act, string file, byte[] byt)
    {
        objeID = id;
        actions = act;
        filename = file;
        bytes = byt;
    }
}

public class DatabaseController : MonoBehaviour {

    public delegate void ResultMethod(ProjectObject project);
    public delegate void ResultMethod2(SceneObject obj);
    public delegate void ResultMethod3(int number);
    public delegate void ResultMethod4();


    private string message;

    private Queue<string> settingsUpdateCommands;
    private bool updatingSettings;

    public void LoadUserLayouts(ResultMethod4 method)
    {
        WWWForm form = new WWWForm();
        form.AddField("owner", ApplicationStaticData.actualProject.owner);
        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "LoadUserLayouts.php", form);
        StartCoroutine(request(w, method));
    }

    private Queue<UpdateActionsObject> actionsUpdateQueue;
    private bool updatingActions;

    private void SaveObjectActions(UpdateActionsObject item)
    {
        WWWForm form = new WWWForm();


        form.AddField("ID", item.objeID.ToString());
        form.AddField("projectID", ApplicationStaticData.actualProject.id);
        form.AddField("sceneID", ApplicationStaticData.actualProject.actualScene);

        form.AddField("actions", item.actions);
        form.AddField("filename", item.filename);

        if  (item.bytes != null)
        {
            form.AddBinaryData("file", item.bytes, item.filename, "audio / mpeg");
        }

        string destinationFile = "UpdateObjectActions.php";

        

        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + destinationFile, form);
        StartCoroutine(changeActionsRequest(w));
    }

    IEnumerator request(WWW w, ResultMethod4 method)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
        Debug.Log(message);

        string[] msg = null;
        msg = message.Split(new string[] { "#####" }, StringSplitOptions.None);
        if (msg.Length > 0)
        {
            int ID;
            for (int i = 0; i < msg.Length; i ++)
            {
                if (Int32.TryParse(msg[i], out ID))
                {
                    ApplicationStaticData.layoutsNumber.Add(ID);
                }
                
            }

            method();
        }
    }

    public void CreateProject(ResultMethod method)
    {
        WWWForm form = new WWWForm();
        form.AddField("Owner", ApplicationStaticData.owner);
 
        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "CreateProject.php", form);
        StartCoroutine(request(w, method));
    }

    public void CreateScene(ResultMethod3 method)
    {
        WWWForm form = new WWWForm();
        form.AddField("projectID", ApplicationStaticData.actualProject.id);

        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "CreateProjectScene.php", form);
        StartCoroutine(request(w, method));
    }

    public void SaveLayout(ResultMethod3 method)
    {
        WWWForm form = new WWWForm();
        form.AddField("Owner", ApplicationStaticData.owner);

        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "CreateLayout.php", form);
        StartCoroutine(request(w, method));
    }

    public void SaveLayoutObject(ObjectsTypes objectType, string number, Vector3 pos, Vector3 rot, Vector3 size, int layID)
    {
        WWWForm form = new WWWForm();
        form.AddField("objectName", (int)objectType);
        form.AddField("objectNumber", number);
        form.AddField("posX", pos.x.ToString());
        form.AddField("posY", pos.y.ToString());
        form.AddField("posZ", pos.z.ToString());

        form.AddField("rotX", rot.x.ToString());
        form.AddField("rotY", rot.y.ToString());
        form.AddField("rotZ", rot.z.ToString());

        form.AddField("sizeX", size.x.ToString());
        form.AddField("sizeY", size.y.ToString());
        form.AddField("sizeZ", size.z.ToString());

        form.AddField("layoutID", layID);


        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "AddLayoutObject.php", form);
        StartCoroutine(request(w));
    }


    public void SaveObject(ObjectsTypes objectType, string number, Vector3 pos, Vector3 rot, Vector3 size, ResultMethod2 meth, byte [] bytes = null, string extension = "", byte [] additionalTexBytes = null)
    {
        Debug.Log("Saving");
        WWWForm form = new WWWForm();
        form.AddField("objectName", (int)objectType);
        form.AddField("objectNumber", number);
        form.AddField("posX", pos.x.ToString());
        form.AddField("posY", pos.y.ToString());
        form.AddField("posZ", pos.z.ToString());

        form.AddField("rotX", rot.x.ToString());
        form.AddField("rotY", rot.y.ToString());
        form.AddField("rotZ", rot.z.ToString());

        form.AddField("sizeX", size.x.ToString());
        form.AddField("sizeY", size.y.ToString());
        form.AddField("sizeZ", size.z.ToString());

        form.AddField("projectID", ApplicationStaticData.actualProject.id);
        form.AddField("sceneID", ApplicationStaticData.actualProject.actualScene);

        if (objectType != ObjectsTypes.object3D && bytes != null)
        {
            Debug.Log(extension);
            form.AddBinaryData("file", bytes, number, "image/" + extension);
        }

        string destinationFile = "AddObject.php";

        if (objectType == ObjectsTypes.object3D)
        {
            if (bytes != null)
            {
                form.AddBinaryData("file", bytes, objectType.ToString() + ".obj", "text/plain");
            }

            if (additionalTexBytes != null)
            {
                form.AddBinaryData("file2", additionalTexBytes, "tex"+extension, "image/" + extension.Substring(1));
            }

            destinationFile = "Add3DObject.php";
        }

        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + destinationFile, form);
        StartCoroutine(request(w, meth, pos, rot, size));
    }

    public void UpdateObject( Vector3 pos, Vector3 rot, Vector3 size, int ID)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);

        form.AddField("posX", pos.x.ToString());
        form.AddField("posY", pos.y.ToString());
        form.AddField("posZ", pos.z.ToString());

        form.AddField("rotX", rot.x.ToString());
        form.AddField("rotY", rot.y.ToString());
        form.AddField("rotZ", rot.z.ToString());

        form.AddField("sizeX", size.x.ToString());
        form.AddField("sizeY", size.y.ToString());
        form.AddField("sizeZ", size.z.ToString());

        form.AddField("projectID", ApplicationStaticData.actualProject.id);
        form.AddField("sceneID", ApplicationStaticData.actualProject.actualScene);

        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "UpdateObject.php", form);
        StartCoroutine(request(w));
    }

    public void DeleteObject(int ID)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("projectID", ApplicationStaticData.actualProject.id);
        form.AddField("sceneID", ApplicationStaticData.actualProject.actualScene);
        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "DeleteObject.php", form);
        StartCoroutine(request(w));
    }

    //public void SaveLayout(int number)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("layoutNumber", number);
    //    WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "ChangeLayout.php", form);
    //    StartCoroutine(request(w));
    //}

    IEnumerator request(WWW w)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
      //  Debug.Log(message);
    }

    IEnumerator changeActionsRequest(WWW w)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
        updatingActions = false;
    }

    IEnumerator request(WWW w, ResultMethod2 method, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
        Debug.Log(message);

        string[] msg = null;
        msg = message.Split(new string[] { "#####" }, StringSplitOptions.None);
        if (msg.Length > 0)
        {
            int ID = Int32.Parse(msg[0]);
            ObjectsTypes typ1 = (ObjectsTypes)Int32.Parse(msg[1]);
            string path = msg[2];

            if (ObjectsTypes.shapeObject == typ1)
            {
                ShapeObject obj = new ShapeObject(path, ID, typ1, pos, rot, scale);
                method(obj);
            }
            else if (ObjectsTypes.object3D == typ1)
            {
                Object3D obj = new Object3D(path, ID, typ1, pos, rot, scale);
                method(obj);
            }
            else {
                VideoObject obj = new VideoObject(path, ID, typ1, pos, rot, scale);
                method(obj);
            }



        }
    }

    IEnumerator request(WWW w, ResultMethod method)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
        Debug.Log(message);

        string[] msg = null;
        msg = message.Split(new string[] { "#####" }, StringSplitOptions.None);
        if (msg.Length > 0)
        {
            int ID = Int32.Parse(msg[0]);
            string auth = msg[1];
            string onwer = msg[2];
            string name = msg[3];
            ProjectObject proj = new ProjectObject(ID, auth, onwer, name);
            proj.SetProjectSettings(true);
            method(proj);
        }
    }

    IEnumerator request(WWW w, ResultMethod3 method)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
        Debug.Log(message);

        int ID;

        if (Int32.TryParse(message, out ID))
        {
            method(ID);
        }
            
    }

    private void Start()
    {
        settingsUpdateCommands = new Queue<string>();
        actionsUpdateQueue = new Queue<UpdateActionsObject>();
    }

    public void AddUpdateActionsObject(UpdateActionsObject item)
    {
        actionsUpdateQueue.Enqueue(item);
    }

    private void UpdateSettings(string command)
    {
        WWWForm form = new WWWForm();
        form.AddField("command", command);
        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "ExecuteSQL.php", form);
        StartCoroutine(requestSettingUpdate(w));
    }

    IEnumerator requestSettingUpdate(WWW w)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
        Debug.Log(message);
        updatingSettings = false;
    }

    private void Update()
    {
        if (settingsUpdateCommands.Count > 0 && !updatingSettings)
        {
            updatingSettings = true;
            UpdateSettings(settingsUpdateCommands.Dequeue());
        }

        if (actionsUpdateQueue.Count > 0 && !updatingActions)
        {
            updatingActions = true;
            SaveObjectActions(actionsUpdateQueue.Dequeue());
        }
    }


    public void AddSettingsUpdateCommand(string command)
    {
        settingsUpdateCommands.Enqueue(command);
    }



}
