using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInToNextOne : MonoBehaviour {

    public Sprite[] pictures;
    public float fadeTime = 1f;
    public float pictureStayTime = 3;

    private bool fadeIn = true;
    private float lastChangeTime=0;
    private int cnt = 0;

    void Update () {

        if (Time.time > pictureStayTime+lastChangeTime)
        {
            if (cnt < pictures.Length-1) cnt++;
            else cnt = 0;
            GetComponent<SpriteRenderer>().sprite = pictures[cnt];
            //ChangePicture();
            lastChangeTime = Time.time;            
        }


        
    }
    /*
    private void ChangePicture()
    {
        if (fadeIn)
        {
            float Fade = Mathf.SmoothDamp(0f, 1f, ref fadeSpeed, fadeTime);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Fade);
        }

        if (!fadeIn)
        {
            float Fade = Mathf.SmoothDamp(1f, 0f, ref fadeSpeed, fadeTime);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Fade);
        }
    }*/
}
