using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Objects3DMenu : GuiMenu
{

    public EnviromentMAnager envir;
    //private int firstPrefabToLoad = 7;
    private int firstPrefabToLoad = 100;

    float emptySize = 0.2f;

    private void Start()
    {
        scroll = GetComponent<VerticalScroll>();
        yAdding = -0.25f;
        startX = 0.15f;
        CreateIcons();
       
    }

    private void CreateIcons()
    {

        ClearIcons();

        GameObject newObject = null;
        int i = 0;
        if (ApplicationStaticData.objects3DInfos != null)
        {
            
            foreach (Object3DInfo info in ApplicationStaticData.objects3DInfos)
            {
                
                newObject = Instantiate(iconPrefab, transform.position, iconPrefab.transform.rotation);
                try
                {
                   
                    newObject.GetComponent<MeshFilter>().mesh = info.mesh;

                    var mf = newObject.GetComponent<MeshFilter>();

                    if (mf != null)
                    {
                        var bounds = mf.mesh.bounds;

                        float max = bounds.extents.x;
                        if (max < bounds.extents.y)
                            max = bounds.extents.y;
                        if (max < bounds.extents.z)
                            max = bounds.extents.z;
                        if (max != 0.0f)
                        {
                            float scale = (emptySize * 0.5f) / max;

                            newObject.transform.localScale = new Vector3(scale, scale, scale);
                        }

                    }

                    if (info.texturesInfos.Count > 0 )
                    {
                        newObject.GetComponent<Renderer>().material.mainTexture = info.texturesInfos[0].tex;
                    }


                    


                    newObject.AddComponent<BoxCollider>();
                    newObject.GetComponent<Object3DSpawningButton>().basePath = info.path + "/" + info.name;
                    newObject.GetComponent<Object3DSpawningButton>().object3DInfo = info;

                    newObject.transform.parent = transform;

                    newObject.transform.localPosition = new Vector3(actualX, actualY, actualZ);

                    i++;
                    if (i == 1 || i == ApplicationStaticData.objects3DInfos.Count)
                    {
                        CheckRaycasting raycasting = newObject.AddComponent<CheckRaycasting>();
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
                    IconShowHideScript showHide = newObject.AddComponent<IconShowHideScript>();
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

                    

                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }


            }
        }

        for (int j = firstPrefabToLoad; j < envir.objectsPrefabs.Length; j++)
        {
            newObject = Instantiate(envir.objectsPrefabs[j], transform.position, envir.objectsPrefabs[j].transform.rotation);

            if (j + 1 == envir.objectsPrefabs.Length)
            {
                CheckRaycasting raycasting = newObject.AddComponent<CheckRaycasting>();
                raycasting.raycastingGameObject = panel;
                    lastIcon = raycasting;

            }

            Destroy(newObject.GetComponent<SceneObjectRaycastMovingScript>());


            newObject.AddComponent<Object3DSpawningButton>();

            newObject.GetComponent<Object3DSpawningButton>().basePath = j.ToString();
            newObject.GetComponent<Object3DSpawningButton>().objectToSpawn = envir.objectsPrefabs[j];
            newObject.transform.parent = transform;

            newObject.transform.tag = "Btn";

            newObject.transform.localPosition = new Vector3(actualX, actualY, actualZ);

            IconShowHideScript showHide = newObject.AddComponent<IconShowHideScript>();
            showHide.SetReferenceObject(panel);

            i++;

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
