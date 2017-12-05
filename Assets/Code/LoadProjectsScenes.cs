using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProjectsScenes : GuiMenu
{
    public GameObject createnewProjectIconPrefab;

    private void OnEnable()
    {
        LoadScenes();
    }

    public void LoadScenes()
    {
        scroll = GetComponent<VerticalScroll>();
        colsCount = 2;
        yAdding = -0.32f;
        startY = actualY = 0.78f;
        startZ = actualZ = -0.25f;
        zAdding = 0.45f;
        ShowUProjectScenes();
    }

    public void ShowUProjectScenes()
    {
        int i = 0;

        ClearButtons();
        foreach (int sceneNumb in ApplicationStaticData.actualProject.projectScenesNumbers)
        {
            GameObject newButton = Instantiate(iconPrefab);
            newButton.transform.parent = scroll.transform;

            newButton.transform.position = scroll.transform.position;
            newButton.transform.localPosition += new Vector3(actualX, actualY, actualZ);

            i++;

            if (i == 1)
            {
                CheckRaycasting raycasting = newButton.AddComponent<CheckRaycasting>();
                raycasting.raycastingGameObject = panel;
                    firstIcon = raycasting;

            }

            IconShowHideScript showHide = newButton.AddComponent<IconShowHideScript>();
            showHide.SetReferenceObject(panel);

            if (i % colsCount == 0)
            {
                actualZ = startZ;
                actualY += yAdding;
            }
            else
            {
                actualZ += zAdding;
            }

            newButton.GetComponent<ChangeSceneBtn>().SetSceneID(sceneNumb);

        }

        if (createnewProjectIconPrefab != null)
        {
            GameObject newButton1 = Instantiate(createnewProjectIconPrefab);
            newButton1.transform.parent = scroll.transform;

            newButton1.transform.position = scroll.transform.position;
            newButton1.transform.localPosition += new Vector3(actualX, actualY, actualZ);

            IconShowHideScript showHide = newButton1.AddComponent<IconShowHideScript>();
            showHide.SetReferenceObject(panel);

            CheckRaycasting raycasting = newButton1.AddComponent<CheckRaycasting>();
            raycasting.raycastingGameObject = panel;
            lastIcon = raycasting;
        }
        
    }

    public void ClearButtons()
    {
        if (scroll != null)
        {
            foreach (Transform child in scroll.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
