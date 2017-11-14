using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGUIButton : MonoBehaviour {

    protected bool isActive;

    public void ActivateObject()
    {
        isActive = true;
        transform.Find("Text").GetComponent<TextMesh>().color = Color.white;
    }

    public void DeactivateObject()
    {
        isActive = false;
        transform.Find("Text").GetComponent<TextMesh>().color = Color.gray;
    }
}
