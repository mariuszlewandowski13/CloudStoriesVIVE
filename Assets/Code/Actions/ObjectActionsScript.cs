using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public bool CheckActionExists(ObjectActionType type)
    {
        foreach (ObjectAction act in objectActions)
        {
            if (act.type == type) return true;
        }

        return false;
    }

    private ObjectAction CheckAndReturnActionTypeExists(ObjectActionType type)
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
        }
    }

    public string GetSQL()
    {
        string sql = "";

        foreach (ObjectAction act in objectActions)
        {
            sql += (((int)act.type).ToString() + ",");
            sql += act.additionalData + ",";
        }

        if (sql.Length > 0)
        {
            sql = sql.Substring(0, sql.Length - 1);
        }
        return sql;
    }
}
