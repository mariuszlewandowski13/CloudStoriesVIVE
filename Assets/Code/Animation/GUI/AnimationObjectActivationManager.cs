using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObjectActivationManager : MonoBehaviour {

    public ObjectAnimationScript objectAnim;

    public void ActivateDeactivateObject(bool activate)
    {
        if (objectAnim != null)
        {
            objectAnim.isActive = activate;
        }
    }

    public void SetActivationObject(ObjectAnimationScript objectA)
    {
        objectAnim = objectA;
        transform.Find("ActiveObject").gameObject.SetActive(true);
        transform.Find("DeactiveObject").gameObject.SetActive(true);

        if (objectAnim.isActive)
        {
            transform.Find("ActiveObject").GetComponent<AnimationActivateObjBtn>().DeactivateObject();
            transform.Find("DeactiveObject").GetComponent<AnimationActivateObjBtn>().ActivateObject();
        }
        else {
            transform.Find("ActiveObject").GetComponent<AnimationActivateObjBtn>().ActivateObject();
            transform.Find("DeactiveObject").GetComponent<AnimationActivateObjBtn>().DeactivateObject();
        }
    }


    public void DeleteActivationObject()
    {
        objectAnim = null;
        transform.Find("ActiveObject").gameObject.SetActive(false);
        transform.Find("DeactiveObject").gameObject.SetActive(false);
    }
}
