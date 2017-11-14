using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterScalingScript : MonoBehaviour {

    public void StartScaling(GameObject objectScale, Vector3 pos, GameObject controller)
    {
        objectScale.GetComponent<ScalingScript>().OnTriggerDown(controller);

    }

}
