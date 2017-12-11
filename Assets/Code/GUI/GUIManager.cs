using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    private GameObject actualSelectedGameObject;

    public GameObject deleteButton;
    public GameObject actions;

    // public AnimationManager animManager;

    public GameObject lockCheckbox;

    public GameObject settings;

    public EnviromentShwoCheckbox settingsShowCheckbox;

    public void SetActualSelectedGameObject(GameObject newSelectedgameObject)
    {
        actualSelectedGameObject = newSelectedgameObject;

        //if (actualSelectedGameObject.GetComponent<ObjectAnimationScript>() != null && animManager != null)
        //{
        //    animManager.SetActualAnimatedObject(actualSelectedGameObject.GetComponent<ObjectAnimationScript>());

        //}


        SetSelectedObjectInfo(actualSelectedGameObject.name);

        ActivateSettings(false);

        ActivateObjectControlButtons(true);

        lockCheckbox.GetComponent<LockCheckBox>().SetCheckboxActive(actualSelectedGameObject.GetComponent<SceneObjectInfo>().isTransformLocked);
    }

    public void RemoveActualSelectedGameObject()
    {
        RemoveObjectInfo();
        //if (actualSelectedGameObject.GetComponent<ObjectAnimationScript>() != null && animManager != null)
        //{
        //    animManager.RemoveActualAnimatedObject();
        //}
        actualSelectedGameObject = null;


        ActivateSettings(true);
        ActivateObjectControlButtons(false);

    }

    public void DeleteActualSelectedGameObject()
    {
        if (actualSelectedGameObject != null)
        {
            RemoveObjectInfo();
            //if (actualSelectedGameObject.GetComponent<ObjectAnimationScript>() != null && animManager != null)
            //{
            //    animManager.RemoveActualAnimatedObject();
            //}

            actualSelectedGameObject.GetComponent<ObjectDatabaseUpdater>().DestroySceneObject();

            Destroy(actualSelectedGameObject);
            actualSelectedGameObject = null;

            ActivateSettings(true);

            ActivateObjectControlButtons(false);
        }
        
    }

    public void ChangeActualObjectLock(bool isActive)
    {
        if (actualSelectedGameObject != null)
        {
            actualSelectedGameObject.GetComponent<SceneObjectInfo>().isTransformLocked = isActive;
        }
       
    }


    private void SetSelectedObjectInfo(string objectName)
    {
        transform.Find("Right Panel").Find("ObjectName").GetComponent<TextMesh>().text = objectName;
    }

    private void RemoveObjectInfo()
    {
        SetSelectedObjectInfo("");
    }

    public void SetProjectID(int id)
    {
        transform.Find("Right Panel").Find("ID").GetComponent<TextMesh>().text = id.ToString();
    }

    public void SetSceneID(int id)
    {
        transform.Find("Right Panel").Find("SID").GetComponent<TextMesh>().text = id.ToString();
    }

    private void ActivateDeleteButton(bool activate)
    {
        deleteButton.SetActive(activate);
    }

    private void ActivateActions(bool activate)
    {
        actions.SetActive(activate);
    }

    private void ActivateLockCheckbox(bool activate)
    {
        lockCheckbox.SetActive(activate);
    }

    private void ActivateObjectControlButtons(bool activate)
    {
        ActivateDeleteButton(activate);
        ActivateLockCheckbox(activate);
        ActivateActions(activate);
    }

    private void ActivateSettings(bool activate)
    {
        settings.SetActive(activate);
    }

    public void ShowHideEnviroment(bool setActive, bool update = true)
    {
        GameObject.Find("SCENE").GetComponent<EnviromentMAnager>().ShowHideEnviroment(setActive);
        settingsShowCheckbox.SetCheckboxActive(setActive);
        if (update)
        {
            ApplicationStaticData.actualProject.projectSettings.isTableActive = setActive;
        }
       
    }
}
