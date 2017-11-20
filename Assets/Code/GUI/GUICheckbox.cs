using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICheckbox : MonoBehaviour {

    public bool isActive;

    public GameObject checkedLock;

    protected void SetIsActive(bool isActive)
    {
        this.isActive = isActive;
        ChangeShowing();
    }

    protected void ChangeActive()
    {
        isActive = !isActive;
        ChangeShowing();
    }


    private void ChangeShowing()
    {
        if (checkedLock != null)
        {
            checkedLock.SetActive(isActive);
        }
    }
}
