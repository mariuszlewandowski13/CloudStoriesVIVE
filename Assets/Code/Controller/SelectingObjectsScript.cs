#region Usings

using UnityEngine;
using System.Collections.Generic;

#endregion

public class SelectingObjectsScript : MonoBehaviour {

    #region Private Properties

    private bool added = false;

    private GameObject actualController;
    private List<GameObject> controllers;

    private bool highlighted = false;

    #endregion

    #region Public Properties

    public bool selected = false;
    public bool active = false;
    public bool guiInfoShown = false;
    #endregion

    #region Methods

    void Awake()
    {
        controllers = new List<GameObject>();
        selected = false;
        active = false;
    }

    void Start()
    {
        GetComponent<ObjectInteractionScript>().ControllerCollision += ControllerCollision; 
    }

    public void ControllerCollision(GameObject gameObj, bool isEnter)
    {
        if (isEnter && !active)
        {
            active = true;
            gameObj.GetComponent<ControllerScript>().OnTriggerDown += OnTriggerDown;
            added = true;
            actualController = gameObj;

        }
        else if (!isEnter && active && added)
        {
            active = false;
            gameObj.GetComponent<ControllerScript>().OnTriggerDown -= OnTriggerDown;
            added = false;
        }
    }

    private void OnTriggerDown(GameObject controller)
    {
            controller.GetComponent<ControllerScript>().SetSelected(gameObject);
    }

    private void FadeOutComponents()
    {
         if (guiInfoShown)
        {
            guiInfoShown = false;
            GameObject.Find("GUI").GetComponent<GUIManager>().RemoveActualSelectedGameObject();


        }

        foreach (Transform child in transform)
        {
            if (child.GetComponent<ObjectsFadeScript>() != null)
            {
                child.GetComponent<ObjectsFadeScript>().DecreaseFadeIn();
            }
        }
    }

    private void Update()
    {
        if ((active || selected) && !highlighted)
        {
            
            //GetComponent<HighlightingSystem.Highlighter>().ReinitMaterials();
            //GetComponent<HighlightingSystem.Highlighter>().ConstantOn(ApplicationStaticData.objectsHighligthing);
            //   if (GetComponent<Renderer>() != null)
            //{
            //    GetComponent<Renderer>().material.color = ApplicationStaticData.objectsHighligthing;
            //}
           
                highlighted = true;
        }
        else if ((!active && !selected) && highlighted)
        {
            
            //GetComponent<HighlightingSystem.Highlighter>().ConstantOff();
            //if (GetComponent<Renderer>() != null)
            //{
            //    GetComponent<Renderer>().material.color = ApplicationStaticData.objectsHighligthingNormalColor;
            //}
            highlighted = false;
        }
        
        
            
    }

    void OnDestroy()
    {
        if (added)
        {
            actualController.GetComponent<ControllerScript>().OnTriggerDown -= OnTriggerDown;
        }
    }

    public void SetAsSelected(GameObject controller)
    {
        if (!controllers.Contains(controller))
        {
            controllers.Add(controller);
            selected = true;
            FadeInComponents();
        }
        
    }

    public void SetAsNonSelected(GameObject controller)
    {
        controllers.Remove(controller);
        if (controllers.Count == 0)
        {
            selected = false;
            FadeOutComponents();
        }    
    }

    private void FadeInComponents()
    {
        if (!guiInfoShown && GameObject.Find("GUI") != null && GameObject.Find("GUI").GetComponent<GUIManager>() != null)
        {
            guiInfoShown = true;
            GameObject.Find("GUI").GetComponent<GUIManager>().SetActualSelectedGameObject(gameObject);
        }


        foreach (Transform child in transform)
        {
            if (child.GetComponent<ObjectsFadeScript>() != null)
            {
                child.GetComponent<ObjectsFadeScript>().IncreaseFadeIn();
            }
        }
    }


    public void ResetSelecting()
    {
        active = false;
        selected = false;
        controllers = new List<GameObject>();
    }
    #endregion

}
