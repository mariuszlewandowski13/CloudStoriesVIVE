using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerScript : MonoBehaviour {
    #region Public Events & Delegates
    public GameObject rotationCenterPrefab;


    public delegate void Interaction(GameObject gameObject);
    private event Interaction ControllerMove;
    private event Interaction TriggerUp;
    private event Interaction TriggerDown;

    List<Interaction> ControllerMoveDelegates = new List<Interaction>();
    List<Interaction> TriggerUpDelegates = new List<Interaction>();
    List<Interaction> TriggerDownDelegates = new List<Interaction>();

    public event Interaction OnControllerMove
    {
        add
        {
            ControllerMove += value;
            ControllerMoveDelegates.Add(value);
        }

        remove
        {
            ControllerMove -= value;
            ControllerMoveDelegates.Remove(value);
        }
    }

    public event Interaction OnTriggerUp
    {
        add
        {
            TriggerUp += value;
            TriggerUpDelegates.Add(value);
        }

        remove
        {
            TriggerUp -= value;
            TriggerUpDelegates.Remove(value);
        }
    }

    public event Interaction OnTriggerDown
    {
        add
        {
            TriggerDown += value;
            TriggerDownDelegates.Add(value);
        }

        remove
        {
            TriggerDown -= value;
            TriggerDownDelegates.Remove(value);
        }
    }

    public void RemoveAllEvents()
    {
        foreach (Interaction eh in TriggerUpDelegates)
        {
            TriggerUp -= eh;
        }

        foreach (Interaction eh in TriggerDownDelegates)
        {
            TriggerDown -= eh;
        }

        foreach (Interaction eh in ControllerMoveDelegates)
        {
            ControllerMove -= eh;
        }


        TriggerDownDelegates.Clear();
        TriggerUpDelegates.Clear();
        ControllerMoveDelegates.Clear();
    }


    public static event Interaction TriggerDownGlobal;

    public GameObject rotationCenter;

    public GameObject pickup = null;

    public List<GameObject> collidingGameObjects;
    public List<GameObject> realCollidingGameObjects;

    #endregion

    public GameObject secondController;


    #region Private Properties

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public SteamVR_TrackedObject trackedObj;
    public GameObject selected;

    private object selectedLock = new object();
    private object pickupLock = new object();


   public bool triggerDown;
   public bool triggerUp;

    public bool gripDown;
    public bool gripPressed;
    public bool gripUp;

    bool previousStatePressed = false;
    bool actualStatePressed = false;

    

    #endregion

    void Start()
    {
        rotationCenter = Instantiate(rotationCenterPrefab);
         trackedObj = transform.parent.parent.GetComponent<SteamVR_TrackedObject>();
        collidingGameObjects = new List<GameObject>();
        realCollidingGameObjects = new List<GameObject>();
    }

    void Update()
    {
        lock(pickupLock)
        {
            triggerDown = controller.GetPressDown(triggerButton);
            triggerUp = controller.GetPressUp(triggerButton);

            gripDown = controller.GetPressDown(gripButton);
            gripPressed = controller.GetPress(gripButton);
            gripUp = controller.GetPressUp(gripButton);

            //  Debug.Log("TriggerDown :" + triggerDown.ToString() + " Pickup: " + pickup);


            try {
                if (triggerDown && TriggerDown != null)
                {
                    TriggerDown(gameObject);
                }

                if (triggerDown && TriggerDownGlobal != null)
                {
                    TriggerDownGlobal(gameObject);
                }

                if (triggerUp && TriggerUp != null)
                {
                    TriggerUp(gameObject);
                }


                if (ControllerMove != null)
                {
                    ControllerMove(gameObject);
                }
            } catch (Exception e)
            {
                RemoveAllEvents();
            } 

        }

    }

    private void LateUpdate()
    {
        if (triggerDown && pickup == null && GetComponent<ControllerRaycastScript>().actualPointing == null && !GetComponent<RaycastMoveScript>().moving)
        {
            DeleteSelection();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("col");

                lock (pickupLock)
                {
                    if (pickup != null)
                    {
                        if (collider.gameObject.GetComponent<ObjectInteractionScript>() != null && !pickup.GetComponent<ObjectInteractionScript>().GetIsSelected())
                        {
                            pickup.GetComponent<ObjectInteractionScript>().SetCollision(false, gameObject);
                            collidingGameObjects.Add(pickup);
                            pickup = collider.gameObject;
                            pickup.GetComponent<ObjectInteractionScript>().SetCollision(true, gameObject);


                        }
                    }
                    else
                    {
                        if (collider.gameObject.GetComponent<ObjectInteractionScript>() != null )
                        {
                            pickup = collider.gameObject;
                            pickup.GetComponent<ObjectInteractionScript>().SetCollision(true, gameObject);
                        }
                    }

        }
            
        

    }


    public GameObject GetPickup()
    {
        GameObject result;
        lock (pickupLock)
        {
            result = pickup;
        }
        return result;

    }


    public void SetSelected(GameObject newSelected)
    {
        lock (selectedLock)
        {
            if (selected != null)
            {
                if (selected != newSelected)
                {
                    if (selected.GetComponent<SelectingObjectsScript>() != null)
                    {
                        selected.GetComponent<SelectingObjectsScript>().SetAsNonSelected(gameObject);
                    }
                    
                    selected = newSelected;
                    selected.GetComponent<SelectingObjectsScript>().SetAsSelected(gameObject);
                }
            }
            else
            {
                selected = newSelected;
                selected.GetComponent<SelectingObjectsScript>().SetAsSelected(gameObject);
            }
        }
    }


    public void DeleteSelection()
    {
        lock (selectedLock)
        {
            if (selected != null && selected.GetComponent<SelectingObjectsScript>() != null)
            {
                selected.GetComponent<SelectingObjectsScript>().SetAsNonSelected(gameObject);
                
            }
            selected = null;
        }

    }

    //void OnTriggerStay(Collider collider)
    //{
    //    if ((collidingGameObjects.Contains(collider.gameObject) || pickup == collider.gameObject) && !realCollidingGameObjects.Contains(collider.gameObject))
    //    {
    //        realCollidingGameObjects.Add(collider.gameObject);
    //    }

    //}

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("col");

        lock (pickupLock)
        {
            if (pickup != null)
            {
                if (pickup == collider.gameObject)
                {
                    pickup.GetComponent<ObjectInteractionScript>().SetCollision(false, gameObject);
                    pickup = null;
                }
            }
        }
    }

    private void CheckCollidingObjects()
    {
        List<GameObject> objToRemove = new List<GameObject>();

        foreach (GameObject obj in collidingGameObjects)
        {
            if (!realCollidingGameObjects.Contains(obj))
            {
                objToRemove.Add(obj);
            }
        }

        foreach (GameObject obj in objToRemove)
        {
            collidingGameObjects.Remove(obj);
        }

        if (pickup != null && !realCollidingGameObjects.Contains(pickup) && !pickup.GetComponent<ObjectInteractionScript>().GetIsSelected())
        {
            pickup.GetComponent<ObjectInteractionScript>().SetCollision(false, gameObject);
            pickup = null;
        }
    }

    private void OnDestroy()
    {
        if (rotationCenter != null)
        {
            Destroy(rotationCenter);
        }
    }


}
