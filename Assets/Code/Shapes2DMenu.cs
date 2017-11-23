using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes2DMenu : MonoBehaviour {

    public CheckRaycasting firstIcon;
    public CheckRaycasting lastIcon;

    public GameObject shapesIconPrefab;

    public Transform panel;

    public float startX = 1.26f;
    public float startY = 0.4f;
    public float startZ = -0.26f;

    public float zAdding = 0.28f;
    public float yAdding = -0.12f;

    public int colsCount = 3;

    public float actualX = 1.26f;
    public float actualY = 0.4f;
    public float actualZ = -0.26f;

    public VerticalScroll scroll;

    private void Start()
    {
        scroll = GetComponent<VerticalScroll>();
        CreateIcons();
    }

    private void Update()
    {
        if (firstIcon.isRaycasting && scroll.canScrollUp)
        {
            scroll.canScrollUp = false;
        } else if (!firstIcon.isRaycasting && !scroll.canScrollUp)
        {
            scroll.canScrollUp = true;
        }
        if (lastIcon.isRaycasting && scroll.canScrollDown)
        {
            scroll.canScrollDown = false;
        }else if (!lastIcon.isRaycasting && !scroll.canScrollDown)
        {
            scroll.canScrollDown = true;
        }

    }

    private void ClearIcons()
    {
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        actualX = startX;
        actualY = startY;
        actualZ = startZ;
    }

    

    private void CreateIcons()
    {
        ClearIcons();
        

        if (ApplicationStaticData.shapesInfos != null)
        {
            int i = 0;
            foreach (ImagesInfo info in ApplicationStaticData.shapesInfos)
            {
                i++;
                GameObject icon = Instantiate(shapesIconPrefab, transform.position, shapesIconPrefab.transform.rotation);

                if (i == 1 || i == ApplicationStaticData.shapesInfos.Count)
                {
                    CheckRaycasting raycasting =  icon.AddComponent<CheckRaycasting>();
                    raycasting.raycastingGameObject = panel;
                    if (i == 1)
                    {
                        firstIcon = raycasting;
                    }
                    else {
                        lastIcon = raycasting;
                    }
                    
                }

                float ratio = (float)info.height / (float)info.width;

                icon.GetComponent<Object2DSpawningButton>().SetObjectTypes(info.path, info.name, ratio);
                icon.transform.parent = transform;

                icon.transform.localPosition = new Vector3(actualX, actualY, actualZ);

                if (i % colsCount == 0)
                {
                    actualZ = startZ;
                    actualY += yAdding;
                }
                else {
                    actualZ += zAdding;
                }

                
            }
        }
    }
}
