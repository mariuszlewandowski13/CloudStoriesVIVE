#region Usings
using UnityEngine;
#endregion


public class ImageMoveScript : MovingScript
{
    #region Private Properties
    private GameObject rotationParent;

    private Transform prevParent;

    private GameObject actualController;

    private ControllerScript actualControllerScript;

    private float posSnapTreshold = 0.05f;
    private float rotSnapTreshold = 15.0f;

    #endregion
    #region Public Properties

    public bool canMove;
    public bool active = false;
    #endregion


    #region Methods
    void Start()
    {
        GetComponent<ObjectInteractionScript>().ControllerCollision += ControllerCollision;
        canMove = true;
    }

    private void ControllerCollision(GameObject gameObj, bool isEnter)
    {
        if (isEnter && !active)
        {
                gameObj.GetComponent<ControllerScript>().OnTriggerDown += OnTriggerDown;
                active = true;
                actualController = gameObj;
            
        }
        else if (!isEnter && !GetComponent<ObjectInteractionScript>().GetIsSelected() && active)
        {
            gameObj.GetComponent<ControllerScript>().OnTriggerDown -= OnTriggerDown;
            active = false;
        }
    }

    private void OnControllerMove(GameObject controller)
    {
        controllerSecondPosition = controllerFirstPosition;
        controllerFirstPosition = controller.transform.position;

        Vector3 pos = controllerFirstPosition;
        Vector3 rot = controller.transform.rotation.eulerAngles;

        if (actualControllerScript.gripPressed)
        {
            pos = ControllingObjectsHelper.CalculatePosSnap(pos, posSnapTreshold);
            rot = ControllingObjectsHelper.CalculateRotSnap(rot, rotSnapTreshold);
        }



        rotationParent.transform.position = pos;
        rotationParent.transform.rotation = Quaternion.Euler(rot);
                gameObject.transform.parent = rotationParent.transform;

    }

    private void OnTriggerDown(GameObject controller)
    {
        GetComponent<ObjectInteractionScript>().SetIsSelected(true);
        if (GetComponent<SceneObjectInfo>() == null || !GetComponent<SceneObjectInfo>().isTransformLocked)
        {
            controller.GetComponent<ControllerScript>().OnTriggerUp += OnTriggerUp;


            controller.GetComponent<ControllerScript>().OnControllerMove += OnControllerMove;
            rotationParent = controller.GetComponent<ControllerScript>().rotationCenter;
            controllerFirstPosition = controllerSecondPosition = controller.transform.position;

            rotationParent.transform.position = controllerFirstPosition;
            rotationParent.transform.rotation = controller.transform.rotation;
            prevParent = gameObject.transform.parent;

            gameObject.transform.parent = rotationParent.transform;

            actualControllerScript = controller.GetComponent<ControllerScript>();

        }
      
        
    }
    private void OnTriggerUp(GameObject controller)
    {
        controller.GetComponent<ControllerScript>().OnTriggerUp -= OnTriggerUp;
        controller.GetComponent<ControllerScript>().OnControllerMove -= OnControllerMove;
        GetComponent<ObjectInteractionScript>().SetIsSelected(false);
        gameObject.transform.parent = prevParent;
       // CheckAndSetToDestroy(gameObject);
        
    }

 

    private void OnDestroy()
    {    
        GetComponent<ObjectInteractionScript>().ControllerCollision -= ControllerCollision;
    }

   

    #endregion
}
