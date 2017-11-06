using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour {
    public Vector3 rotAxis;
    public float speed = 1;
    public float startTime;
	void Update () {
        if (Time.time> startTime)        this.transform.RotateAround(rotAxis, speed*Time.deltaTime);
	}
}
