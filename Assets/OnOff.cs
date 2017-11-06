using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour {

    public float turnOnTime;
    public float turnOffTime;

    void Update () {
        if (turnOnTime != 0 && Time.time > turnOnTime) TurnOn();
        if (turnOffTime != 0 && Time.time > turnOffTime) TurnOff();
    }

    public void TurnOn()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void TurnOff()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
