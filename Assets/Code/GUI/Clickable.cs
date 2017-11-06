using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour, IRaycastPointable, IClickable {

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
        GetComponent<Renderer>().material.color = normalColor;

    }

    private void showHighLight()
    {
        GetComponent<Renderer>().material.color = highlightedColor;
    }


    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        Debug.Log("Clicked");
    }

    private void Start()
    {
        normalColor = GetComponent<Renderer>().material.color;
    }
}
