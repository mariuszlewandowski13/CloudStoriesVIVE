#region Usings

using UnityEngine;
using System.Collections.Generic;

#endregion


public class ControlObjects : MonoBehaviour
{

    public static GameObject scroll;
    public static object scrolLock = new object();
    public static GameObject scrollingGameObject;


    public bool click;

    public static bool SetScrollingObject(GameObject newScrolling)
    {
        bool result = false;

        lock (scrolLock)
        {
            if (scrollingGameObject == null && scroll != null)
            {
                scrollingGameObject = newScrolling;
                result = true;
            }
        }

        return result;
    }

    public static GameObject UnsetScrollingObject(GameObject newScrolling)
    {
        lock (scrolLock)
        {
            if (scrollingGameObject == newScrolling)
            {
                scrollingGameObject = null;
            }
        }

        return scrollingGameObject;
    }

    //private class ControllerState
    //{
    //    public InteractionSourceHandedness Handedness;
    //    public Vector3 PointerPosition;
    //    public Quaternion PointerRotation;
    //    public Vector3 GripPosition;
    //    public Quaternion GripRotation;
    //    public bool Grasped;
    //    public bool MenuPressed;
    //    public bool SelectPressed;
    //    public float SelectPressedAmount;
    //    public bool ThumbstickPressed;
    //    public Vector2 ThumbstickPosition;
    //    public bool TouchpadPressed;
    //    public bool TouchpadTouched;
    //    public Vector2 TouchpadPosition;
    //}

    //private Dictionary<uint, ControllerState> controllers;


    //#region Methods

    //private void Awake()
    //{
    //    controllers = new Dictionary<uint, ControllerState>();

    //    InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;

    //    InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
    //    InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;

    //}
   


    //private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
    //{
    //    Debug.LogFormat("{0} {1} Detected", obj.state.source.handedness, obj.state.source.kind);

    //    if (obj.state.source.kind == InteractionSourceKind.Controller && !controllers.ContainsKey(obj.state.source.id))
    //    {
    //        controllers.Add(obj.state.source.id, new ControllerState { Handedness = obj.state.source.handedness });
    //    }
    //}

    //private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
    //{
    //    Debug.LogFormat("{0} {1} Lost", obj.state.source.handedness, obj.state.source.kind);



    //    controllers.Remove(obj.state.source.id);
    //}

    //private void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
    //{
    //    ControllerState controllerState;
    //    if (controllers.TryGetValue(obj.state.source.id, out controllerState))
    //    {
    //        obj.state.sourcePose.TryGetPosition(out controllerState.PointerPosition, InteractionSourceNode.Pointer);
    //        obj.state.sourcePose.TryGetRotation(out controllerState.PointerRotation, InteractionSourceNode.Pointer);
    //        obj.state.sourcePose.TryGetPosition(out controllerState.GripPosition, InteractionSourceNode.Grip);
    //        obj.state.sourcePose.TryGetRotation(out controllerState.GripRotation, InteractionSourceNode.Grip);

    //        controllerState.Grasped = obj.state.grasped;
    //        controllerState.MenuPressed = obj.state.menuPressed;
    //        controllerState.SelectPressed = obj.state.selectPressed;
    //        controllerState.SelectPressedAmount = obj.state.selectPressedAmount;
    //        controllerState.ThumbstickPressed = obj.state.thumbstickPressed;
    //        controllerState.ThumbstickPosition = obj.state.thumbstickPosition;
    //        controllerState.TouchpadPressed = obj.state.touchpadPressed;
    //        controllerState.TouchpadTouched = obj.state.touchpadTouched;
    //        controllerState.TouchpadPosition = obj.state.touchpadPosition;
    //    }
    //}


    //private void Update()
    //{
        
    //    foreach (ControllerState controllerState in controllers.Values)
    //    {
    //        ControllerScript controllerControlObject;
    //        if (controllerState.Handedness == InteractionSourceHandedness.Right && transform.Find("RightController") != null)
    //        {
    //            controllerControlObject = transform.Find("RightController").Find("Controller(Clone)").Find("ControlObject").GetComponent<ControllerScript>();
    //        }
    //        else if (controllerState.Handedness == InteractionSourceHandedness.Left && transform.Find("LeftController") != null)
    //        {
    //            controllerControlObject = transform.Find("LeftController").Find("Controller(Clone)").Find("ControlObject").GetComponent<ControllerScript>();

    //        }
    //        else {
    //            controllerControlObject = new ControllerScript();
    //        }

    //        bool TriggerBtn = controllerState.SelectPressed;
    //        Debug.Log(TriggerBtn);
    //        controllerControlObject.SetTriggerState(TriggerBtn);

           
    //    }
    //}



    //#endregion
}