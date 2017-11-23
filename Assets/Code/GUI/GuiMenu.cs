using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiMenu : MonoBehaviour {

    public CheckRaycasting firstIcon;
    public CheckRaycasting lastIcon;

    public VerticalScroll scroll;

    public GameObject iconPrefab;

    public Transform panel;

    protected float startX = 0.07f;
    protected float startY = 0.85f;
    protected float startZ = -0.3f;

    protected float zAdding = 0.3f;
    protected float yAdding = -0.18f;

    protected int colsCount = 3;

    protected float actualX = 0.07f;
    protected float actualY = 0.85f;
    protected float actualZ = -0.3f;

    private void Update()
    {
        if (firstIcon.isRaycasting && scroll.canScrollUp)
        {
            scroll.canScrollUp = false;
        }
        else if (!firstIcon.isRaycasting && !scroll.canScrollUp)
        {
            scroll.canScrollUp = true;
        }
        if (lastIcon.isRaycasting && scroll.canScrollDown)
        {
            scroll.canScrollDown = false;
        }
        else if (!lastIcon.isRaycasting && !scroll.canScrollDown)
        {
            scroll.canScrollDown = true;
        }

    }

    protected void ClearIcons()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        actualX = startX;
        actualY = startY;
        actualZ = startZ;
    }


}
