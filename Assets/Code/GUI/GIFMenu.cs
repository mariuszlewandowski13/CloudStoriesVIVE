using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIFMenu : GuiMenu {


    private void Start()
    {
        scroll = GetComponent<VerticalScroll>();
        CreateIcons();
    }

    private void CreateIcons()
    {
        ClearIcons();


        if (ApplicationStaticData.gifsInfos != null)
        {
            int i = 0;
            foreach (FileInfoStruct info in ApplicationStaticData.gifsInfos)
            {
                i++;
                GameObject icon = Instantiate(iconPrefab, transform.position, iconPrefab.transform.rotation);

                if (i == 1 || i == ApplicationStaticData.gifsInfos.Count)
                {
                    CheckRaycasting raycasting = icon.AddComponent<CheckRaycasting>();
                    raycasting.raycastingGameObject = panel;
                    if (i == 1)
                    {
                        firstIcon = raycasting;
                    }
                    else
                    {
                        lastIcon = raycasting;
                    }

                }

                icon.GetComponent<VideoSpawningButton>().SetObjectTypes("file://" + info.path, info.name);
                icon.transform.parent = transform;

                icon.transform.localPosition = new Vector3(actualX, actualY, actualZ);

                if (i % colsCount == 0)
                {
                    actualZ = startZ;
                    actualY += yAdding;
                }
                else
                {
                    actualZ += zAdding;
                }


            }
        }
    }
}
