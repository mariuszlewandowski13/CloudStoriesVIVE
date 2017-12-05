using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconShowHideScript : MonoBehaviour {

    public bool isActive = true;

    private Transform referenceObject;
	
	void Update () {
        if (referenceObject != null)
        {
            bool toActivate = CheckObjectProperDistance();
            Activation(toActivate);
        }

	}

    public void Activation(bool toActivate)
    {
        if (toActivate && !isActive)
        {
            ActivateObject();
        }
        else if (!toActivate && isActive)
        {
            DeactivateObject();
        }
    }

    public bool CheckObjectProperDistance()
    {
        if (Mathf.Abs(transform.position.y -referenceObject.transform.position.y) > referenceObject.transform.lossyScale.y / 2.0f)
        {
            return false;
        }
        else {
            return true;
        }
    }

    public void SetReferenceObject(Transform reference)
    {
        referenceObject = reference;
    }

    private void ActivateObject()
    {
        if (GetComponent<BoxCollider>() == null) gameObject.AddComponent<BoxCollider>();
        if (GetComponent<MeshRenderer>() != null) GetComponent<MeshRenderer>().enabled = true;
        isActive = true;
    }

    private void DeactivateObject()
    {
        if (GetComponent<BoxCollider>() != null) Destroy(GetComponent<BoxCollider>());
        if (GetComponent<MeshRenderer>() != null) GetComponent<MeshRenderer>().enabled = false;
        isActive = false;
    }
}
