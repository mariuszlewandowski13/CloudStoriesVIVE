using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRaycastScript : RaycastBase {

    private bool isPointing;

    private RaycastHit hit;

    public Transform actualPointing;

    private ControllerScript controller;

    public bool isActive = true;

    public bool scrollingStarted;

    public float triggerDownClickedTime;
    public Vector3 lastPosition;

    public bool click;

    void Start () {
        controller = GetComponent<ControllerScript>();
        lastPosition = transform.position;
    }

	void Update () {
        if (isActive && controller.pickup == null)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            Physics.Raycast(ray, out hit, 5);

            bool pressedDown = controller.triggerDown;
            bool pressedUp = controller.triggerUp;

            if (!scrollingStarted && hit.transform != null && hit.transform.GetComponent<IClickable>() != null && (hit.transform.tag == "Btn" || hit.transform.tag == "SceneObject" || hit.transform.tag == "Tools"))
            {
                isPointing = true;
                hitPoint = hit.point;
                CursorOn();

                if (hit.transform != actualPointing)
                {
                    if (actualPointing != null && actualPointing.GetComponent<IRaycastPointable>() != null)
                    {
                        actualPointing.GetComponent<IRaycastPointable>().Highlight(false);
                    }

                    actualPointing = hit.transform;
                    if (actualPointing.GetComponent<IRaycastPointable>() != null)
                    {
                        actualPointing.GetComponent<IRaycastPointable>().Highlight(true);
                    }
                }


                if (pressedDown || click)
                {
                    actualPointing.GetComponent<IClickable>().Clicked(hitPoint, gameObject);
                    click = false;
                }


            } else if (hit.transform != null && hit.transform.tag == "ScrollingPanel")
            {
                isPointing = true;
                hitPoint = hit.point;
                CursorOn();
                if (pressedDown)
                {
                    if (ControlObjects.SetScrollingObject(gameObject))
                    {
                        scrollingStarted = true;
                    }
                }
            }
            else if (isPointing)
            {
                CursorOff();
                isPointing = false;

                if (actualPointing != null)
                {
                    if (actualPointing.GetComponent<IRaycastPointable>() != null)
                    {
                        actualPointing.GetComponent<IRaycastPointable>().Highlight(false);
                    }

                    actualPointing = null;
                }
            }

            if (scrollingStarted)
            {
                if (pressedUp)
                {
                    ControlObjects.UnsetScrollingObject(gameObject);
                    scrollingStarted = false;
                }
                else if (ControlObjects.scroll != null)
                {
                    ControlObjects.scroll.GetComponent<IScrollable>().GetControllerChange(lastPosition - transform.position);
                }
            }

            lastPosition = transform.position;
        }
        else  if (isPointing)
        {
            CursorOff();
            isPointing = false;

            if (actualPointing != null)
            {
                if (actualPointing.GetComponent<IRaycastPointable>() != null)
                {
                    actualPointing.GetComponent<IRaycastPointable>().Highlight(false);
                }

                actualPointing = null;
            }
        }

    }

}
