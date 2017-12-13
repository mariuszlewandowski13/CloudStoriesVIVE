using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum ObjectActionType
{
    animation,
    changeScene,
    URL,
    audio
}

public class ObjectAction
{
    public bool isActive;
    public ObjectActionType type;
    public string additionalData;

    public ObjectAction(bool active, ObjectActionType newType, string addData)
    {
        isActive = active;
        type = newType;
        additionalData = addData;
    }
}

public class ObjectActionsScript : MonoBehaviour {


    private List<ObjectAction> objectActions;

    private bool audioUpdated;

	void Start () {
        objectActions = new List<ObjectAction>();
	}

    public void AddAction(ObjectAction newAction)
    {
        ObjectAction act = CheckAndReturnActionTypeExists(newAction.type);

        if (act != null)
        {
            act.additionalData = newAction.additionalData;
        }
        else {
            objectActions.Add(newAction);
        }

        if (newAction.type == ObjectActionType.audio) audioUpdated = true;
        UpdateDatabase();
    }

    public bool CheckActionExists(ObjectActionType type)
    {
        foreach (ObjectAction act in objectActions)
        {
            if (act.type == type) return true;
        }

        return false;
    }

    public ObjectAction CheckAndReturnActionTypeExists(ObjectActionType type)
    {
        foreach (ObjectAction act in objectActions)
        {
            if (act.type == type) return act;
        }

        return null;
    }

    public void RemoveAction(ObjectActionType type)
    {
        ObjectAction act = CheckAndReturnActionTypeExists(type);

        if (act != null)
        {
            objectActions.Remove(act);
            UpdateDatabase();
        }
        
    }

    public string GetSQL()
    {
        string sql = "";

        foreach (ObjectAction act in objectActions)
        {
            sql += (((int)act.type).ToString() + "#####");
            sql += act.additionalData + "#####";
        }

        if (sql.Length > 0)
        {
            sql = sql.Substring(0, sql.Length - 5);
        }
        return sql;
    }

    public void UpdateDatabase()
    {
        if (GameObject.Find("LoadScene") != null)
        {
            string actions = GetSQL();
            string filename = "";
            byte[] bytes = null;
            ObjectAction audioAction = CheckAndReturnActionTypeExists(ObjectActionType.audio);
            if (audioAction != null && audioUpdated)
            {
                audioUpdated = false;
                filename = audioAction.additionalData;
                bytes = File.ReadAllBytes(ApplicationStaticData.audioPath + filename);
            }
            GameObject.Find("LoadScene").GetComponent<DatabaseController>().AddUpdateActionsObject(new UpdateActionsObject(GetComponent<SceneObjectInfo>().obj.ID, actions, filename, bytes));

        }
    }
}
