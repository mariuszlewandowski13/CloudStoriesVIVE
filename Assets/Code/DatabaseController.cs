using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DatabaseController : MonoBehaviour {

    public delegate void ResultMethod(ProjectObject project);
    public delegate void ResultMethod2(SceneObject obj);
    public delegate void ResultMethod3(int number);
    public delegate void ResultMethod4();


    private string message;

    public void LoadUserLayouts(ResultMethod4 method)
    {
        WWWForm form = new WWWForm();
        form.AddField("owner", ApplicationStaticData.actualProject.owner);
        WWW w = new WWW(ApplicationStaticData.serverScriptsPath + "LoadUserLayouts.php", form);
        StartCoroutine(request(w, method));
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
            }else if (ObjectsTypes.object3D == typ1)
            {
                Object3D obj = new Object3D(path, ID, typ1, pos, rot, scale);
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



}
