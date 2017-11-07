using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointableGUIButton : MonoBehaviour, IRaycastPointable {


    private bool highlighted;

    private Color normalColor;
    private Color highlightedColor = new Color(0.16f, 0.16f, 0.16f);
    private bool Highlighted
    {
        get
        {
            return highlighted;
        }

        set
        {
            highlighted = value;
            if (highlighted == true) showHighLight();
            else hideHighlight();

        }
    }

    public void Highlight(bool active)
    {
        Highlighted = active;
    }

    private void hideHighlight()
    {
        if (GetComponent<Renderer>() != null && GetComponent<Renderer>().material.HasProperty("_Color"))
        {
            GetComponent<Renderer>().material.color = normalColor;
        }
        

    }

    private void showHighLight()
    {
        if (GetComponent<Renderer>() != null && GetComponent<Renderer>().material.HasProperty("_Color"))
        {
            GetComponent<Renderer>().material.color = highlightedColor;
        }
       
    }

    private void Start()
    {
        if (GetComponent<Renderer>() != null && GetComponent<Renderer>().material.HasProperty("_Color"))
        {
            normalColor = GetComponent<Renderer>().material.color;
        }
        
    }

}
