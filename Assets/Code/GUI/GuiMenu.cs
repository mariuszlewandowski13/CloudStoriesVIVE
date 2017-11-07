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
        if ((firstIcon == null || firstIcon.isRaycasting) && scroll.canScrollUp)
        {
            scroll.canScrollUp = false;
        }
        if ((lastIcon == null || lastIcon.isRaycasting) && scroll.canScrollDown)
        {
            scroll.canScrollDown = false;
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
