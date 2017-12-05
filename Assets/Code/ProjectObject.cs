using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectObjectSettings
{

    public delegate void UpdateSettingsDelegate();

    private UpdateSettingsDelegate updateSetttingsDel;

    private bool _isTableActive;
    public bool isTableActive
    {
        get {
            return _isTableActive;
        }

        set {
            _isTableActive = value;
            updateSetttingsDel();
        }
    }

    public ProjectObjectSettings(bool tableActive, UpdateSettingsDelegate updSetDel)
    {
        _isTableActive = tableActive;
        this.updateSetttingsDel = updSetDel;
    }

    public string GetSettingsAsSQL()
    {
        return " SETTINGS_TABLE =" + (isTableActive ? "1" : "0") + " ";
    }

   
}

public class ProjectObject  {

    public int id;
    public string authID;
    public string owner;
    public string name;

    public List<int> projectScenesNumbers;

    public int actualScene;

    public ProjectObjectSettings projectSettings;

    public ProjectObject(int id, string auth, string own, string nam)
    {
        this.id = id;
        authID = auth;
        owner = own;
        name = nam;

        projectScenesNumbers = new List<int>();
        actualScene = 1;
    }

    public void SetProjectSettings(bool isTableActive)
    {
        projectSettings = new ProjectObjectSettings(isTableActive, UpdateSettingsInDb);
    }

    public string GetSettingsUpdateSQLCommand()
    {
        return "UPDATE PROJECT SET " + projectSettings.GetSettingsAsSQL() + " WHERE ID =" + id.ToString();
    }

    public void UpdateSettingsInDb()
    {
        GameObject.Find("LoadScene").GetComponent<DatabaseController>().AddSettingsUpdateCommand(GetSettingsUpdateSQLCommand());
    }
}
