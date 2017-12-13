using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AddingURLManager : MonoBehaviour {

    public GUIManager manager;

    private string _urlToLoad;
    public string urlToLoad
    {
        get
        {
            return _urlToLoad;
        }

        set
        {
            _urlToLoad = value;
            UpdateURLPresention();
            UpdateCreateBtnPresention();
        }
    }
    public GameObject createButton;
    public GameObject pasteButton;
    public GameObject enterTextButton;


    private void Start()
    {
        ClearURL();
    }

    public void PasteText()
    {
        urlToLoad = GUIUtility.systemCopyBuffer;

    }


    public void EnterText()
    {
        StartTyping();
    }

    public void SaveURL()
    {
        if (manager != null)
        {
            manager.actualSelectedGameObject.GetComponent<ObjectActionsScript>().AddAction(new ObjectAction(true, ObjectActionType.URL, _urlToLoad));
            manager.CancelActivateActions();
        }
    }

    private void UpdateCreateBtnPresention()
    {
        if (urlToLoad == "")
        {
            createButton.GetComponent<AddURLBtn>().DeactivateObject();
        }
        else
        {
            createButton.GetComponent<AddURLBtn>().ActivateObject();
        }
    }

    private void UpdateURLPresention()
    {
        transform.Find("URLValue").GetComponent<TextMesh>().text = urlToLoad;
    }

    private void ClearURL()
    {
        urlToLoad = "";
    }

    public void StartTyping()
    {
        SteamVR.instance.overlay.ShowKeyboard(0, 0, "Description", 256, urlToLoad, true, 0);

        Transform newKeyboardTransform = GameObject.Find("KeyboardDock").transform;

        Vector3 worldPosition = GameObject.Find("[CameraRig]").transform.position;
        Vector3 worldrot = GameObject.Find("[CameraRig]").transform.Find("Camera (eye)").rotation.eulerAngles;

        newKeyboardTransform.position = new Vector3(transform.position.x - worldPosition.x, (transform.position.y - worldPosition.y) / 2.0f, transform.position.z - worldPosition.z);
        newKeyboardTransform.rotation = Quaternion.Euler(0.0f, worldrot.y, 0.0f);

        HmdMatrix34_t keyboardTrackingOrigin = SteamVR_Utils.RigidTransform.FromLocal(newKeyboardTransform).ToHmdMatrix34();
        SteamVR.instance.overlay.SetKeyboardTransformAbsolute(ETrackingUniverseOrigin.TrackingUniverseStanding, ref keyboardTrackingOrigin);


        SteamVR_Events.System(EVREventType.VREvent_KeyboardCharInput).Listen(OnKeyboard);
        SteamVR_Events.System(EVREventType.VREvent_KeyboardClosed).Listen(EndEnteringText);

    }


    private void OnKeyboard(VREvent_t evt)
    {


        if ((char)evt.data.keyboard.cNewInput0 == '\b')
        {
            if (urlToLoad.Length > 0)
            {
                urlToLoad = urlToLoad.Substring(0, urlToLoad.Length - 1);
            }
        }
        else if ((char)evt.data.keyboard.cNewInput0 == '\x1b')
        {
            SteamVR.instance.overlay.HideKeyboard();
        }
        else if (evt.data.keyboard.cNewInput0 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput0;
        }

        if (evt.data.keyboard.cNewInput1 != 0 && evt.data.keyboard.cNewInput1 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput1;
        }
        if (evt.data.keyboard.cNewInput2 != 0 && evt.data.keyboard.cNewInput2 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput2;
        }
        if (evt.data.keyboard.cNewInput3 != 0 && evt.data.keyboard.cNewInput3 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput3;
        }
        if (evt.data.keyboard.cNewInput4 != 0 && evt.data.keyboard.cNewInput4 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput4;
        }
        if (evt.data.keyboard.cNewInput5 != 0 && evt.data.keyboard.cNewInput5 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput5;
        }
        if (evt.data.keyboard.cNewInput6 != 0 && evt.data.keyboard.cNewInput6 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput6;
        }
        if (evt.data.keyboard.cNewInput7 != 0 && evt.data.keyboard.cNewInput7 < 128)
        {
            urlToLoad += (char)evt.data.keyboard.cNewInput7;
        }
    }

    private void EndEnteringText(VREvent_t evt)
    {

    }
}
