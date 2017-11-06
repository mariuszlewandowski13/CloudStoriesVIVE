using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    public void SetSelectedObjectInfo(string objectName)
    {
        transform.Find("Right Panel").Find("ObjectName").GetComponent<TextMesh>().text = objectName;
    }

    public void RemoveObjectInfo()
    {
        SetSelectedObjectInfo("");
    }

    public void SetProjectID(int id)
    {
        transform.Find("Right Panel").Find("ID").GetComponent<TextMesh>().text = id.ToString();
    }
}
