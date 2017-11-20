#region Usings
using UnityEngine;
#endregion


public class ImageMoveScript : MovingScript
{
    #region Private Properties
    private GameObject rotationParent;

    private Transform prevParent;

    private GameObject actualController;

    public bool active = false;

    #endregion
    #region Public Properties

    public bool canMove;

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

        rotationParent.transform.position = controllerFirstPosition;
        rotationParent.transform.rotation = controller.transform.rotation;
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
