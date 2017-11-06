using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterScalingScript : MonoBehaviour {


    private ControllerScript controller;

    private GameObject spawnedObject;

    private bool spawning;

    private ObjectsTypes spawnedObjectType;



    private RaycastHit hit;

    void Start()
    {
       

    }

    public void StartScaling()
    {

      


    }

    private void Update()
    {
        if (spawning)
        {

           

            if (controller.triggerUp)
            {
                StopSpawning();
            }
        }
    }


    public void StopSpawning()
    {
        
    }
}
