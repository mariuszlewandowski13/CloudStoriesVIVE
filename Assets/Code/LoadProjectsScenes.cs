using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProjectsScenes : GuiMenu
{
    public GameObject createnewProjectIconPrefab;

    public bool changeScene;
    public bool chooseSceneForAction;

    public GUIManager manager;

    private void OnEnable()
    {
        if (changeScene)
        {
            LoadScenes();
        }
        if (chooseSceneForAction)
        {
            LoadScenesForActions();
        }
        
    }

    public void PrepareStartPositions()
    {
        colsCount = 2;
        yAdding = -0.32f;
    }

    public void LoadScenesForActions()
    {
        scroll = GetComponent<VerticalScroll>();
        PrepareStartPositions();
        startY = actualY = 0.35f;
        startX = actualX = 0.25f;
        startZ = actualZ = 0.1f;
        xAdding = 0.45f;
        ShowUProjectScenes();
    }

    public void LoadScenes()
    {
        scroll = GetComponent<VerticalScroll>();
        PrepareStartPositions();
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

            
            


            if (changeScene)
            {
                if (i % colsCount == 0)
                {

                    actualZ = startZ;
                    actualY += yAdding;
                }
                else
                {
                    actualZ += zAdding;
                }

                newButton.AddComponent<ChangeSceneBtn>();
                newButton.GetComponent<ChangeSceneBtn>().SetSceneID(sceneNumb);
            }

            if (chooseSceneForAction)
            {
                if (i % colsCount == 0)
                {

                    actualX = startX;
                    actualY += yAdding;
                }
                else
                {
                    actualX -= xAdding;
                }

                newButton.AddComponent<ChangeSceneActionBtn>();
                newButton.GetComponent<ChangeSceneActionBtn>().SetSceneID(sceneNumb, manager);
                newButton.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            }
            

        }

        if (createnewProjectIconPrefab != null)
        {
            GameObject newButton1 = Instantiate(createnewProjectIconPrefab);
            newButton1.transform.parent = scroll.transform;

            newButton1.transform.position = scroll.transform.position;
            newButton1.transform.localPosition += new Vector3(actualX, actualY, actualZ);
            newButton1.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));

            if (changeScene)
            {
                newButton1.AddComponent<CreateNewSceneButton>();
            }

            if (chooseSceneForAction)
            {
                newButton1.AddComponent<CancelChoosingScene>().SetGuiManager(manager);

            }


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
