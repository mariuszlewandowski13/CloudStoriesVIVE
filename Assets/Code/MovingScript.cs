using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {

    protected Vector3 controllerFirstPosition;
    protected Vector3 controllerSecondPosition;


    public float treshold = 0.05f;

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
