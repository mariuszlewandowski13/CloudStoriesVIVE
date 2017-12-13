using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : GuiMenu
{
    public GameObject CanceIconPrefab;

    public GUIManager manager;

    private void OnEnable()
    {
        LoadSounds();
    }

    public void LoadSounds()
    {
        scroll = GetComponent<VerticalScroll>();
        startY = actualY = 0.58f;
        startX = actualX = 0.4f;
        startZ = actualZ = 0.1f;
        colsCount = 1;
        yAdding = -0.1f;
        ShowUProjectScenes();
    }

    public void ShowUProjectScenes()
    {
        int i = 0;

        ClearButtons();
        if (ApplicationStaticData.audioInfos != null)
        {
            foreach (FileInfoStruct fileInfo in ApplicationStaticData.audioInfos)
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

                newButton.AddComponent<AddAudioBtn>();
                newButton.GetComponent<AddAudioBtn>().SetSoundName(fileInfo.name, manager);


            }

            if (CanceIconPrefab != null)
            {
                GameObject newButton1 = Instantiate(CanceIconPrefab);
                newButton1.transform.parent = scroll.transform;

                newButton1.transform.position = scroll.transform.position;
                actualY += yAdding;
                actualY += yAdding;
                actualX = 0.15f;
                newButton1.transform.localPosition += new Vector3(actualX, actualY, actualZ);
                newButton1.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));

                newButton1.AddComponent<CancelChoosingScene>().SetGuiManager(manager);



                IconShowHideScript showHide = newButton1.AddComponent<IconShowHideScript>();
                showHide.SetReferenceObject(panel);

                CheckRaycasting raycasting = newButton1.AddComponent<CheckRaycasting>();
                raycasting.raycastingGameObject = panel;
                lastIcon = raycasting;
            }
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
