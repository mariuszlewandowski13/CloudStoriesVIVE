using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DatabaseController))]
public class CreateProjectScript : MonoBehaviour {

	
	void Start () {
        CreateProject();
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
	
}
