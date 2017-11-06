using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMoveScript : RaycastBase{

    protected Vector3 controllerFirstPosition;
    protected Vector3 controllerSecondPosition;


    public float treshold = 0.05f;

    private ControllerScript controller;

    private GameObject movingObject;
    private Transform movingObejctPreviousParent;

    private bool moving;


    private RaycastHit hit;

    private float raycasterLength = 1.0f;

    private GameObject rotationCenter;


    void Start()
    {
        controller = GetComponent<ControllerScript>();
    }

    public void StartMoving(GameObject objectToMove, Vector3 pos)
    {

        moving = true;
        movingObject = objectToMove;

        raycasterLength = Vector3.Distance(pos, transform.position);


        rotationCenter = new GameObject();
        rotationCenter.transform.position = transform.position;
        rotationCenter.transform.rotation = transform.rotation;
       // rotationCenter.transform.LookAt(gameObject.transform);

       movingObejctPreviousParent = movingObject.transform.parent;

        movingObject.transform.parent = rotationCenter.transform;


        GetComponent<ControllerRaycastScript>().isActive = false;

        GetComponent<ControllerScript>().SetSelected(movingObject);

        controllerFirstPosition = controllerSecondPosition = transform.position;
    }

    private void Update()
    {
        if (moving)
        {
            movingObject.transform.parent = null;

            movingObject.SetActive(false);

            Ray ray = new Ray(transform.position, transform.forward);

            Physics.Raycast(ray, out hit, raycasterLength);
            if (hit.transform != null)
            {
                hitPoint = hit.point;
            }
            else
            {
                hitPoint = transform.forward * raycasterLength + transform.position;
            }
            CursorOn();

            
            movingObject.transform.position = hitPoint;
            rotationCenter.transform.position = transform.position;
            movingObject.transform.parent = rotationCenter.transform;

            
            rotationCenter.transform.rotation = transform.rotation;
            movingObject.SetActive(true);

            controllerSecondPosition = controllerFirstPosition;
            controllerFirstPosition = controller.transform.position;

            if (controller.triggerUp)
            {
                StopMoving();
            }
        }
    }


    public void StopMoving()
    {
        moving = false;
        movingObject.transform.parent = movingObejctPreviousParent;

        CheckAndSetToDestroy(movingObject);

        movingObject = null;
        movingObejctPreviousParent = null;

        Destroy(rotationCenter);

        GetComponent<ControllerRaycastScript>().isActive = true;
        CursorOff();

        
    }

    

    protected void CheckAndSetToDestroy(GameObject movingObject)
    {
        float distance = Vector3.Distance(controllerFirstPosition, controllerSecondPosition);
        if (distance > treshold)
        {
            Rigidbody rigidbod = movingObject.AddComponent<Rigidbody>();
            rigidbod.AddForce((controllerFirstPosition - controllerSecondPosition) * 8000.0f, ForceMode.Force);
            rigidbod.mass = 0.01f;
            rigidbod.useGravity = true;
            rigidbod.detectCollisions = false;
            rigidbod.interpolation = RigidbodyInterpolation.Interpolate;
            DestroyImmediate(movingObject.GetComponent<BoxCollider>());
            Transform scaleHandler = movingObject.transform.Find("ScaleHandler");

            if (scaleHandler != null)
            {
                DestroyObjectAndChildrenColliders(scaleHandler);
            }

            movingObject.GetComponent<ObjectDatabaseUpdater>().DestroySceneObject();

            Destroy(movingObject, 5.0f);
        }

    }

    protected void DestroyObjectAndChildrenColliders(Transform gameObject)
    {
        foreach (Transform child in gameObject)
        {
            if (child.GetComponent<Collider>() != null) child.GetComponent<Collider>().enabled = false;
        }
    }



}
