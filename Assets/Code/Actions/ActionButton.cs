using UnityEngine;

public class ActionButton : MonoBehaviour {

    private bool isActive;
    private string actionValue;
    public bool IsActive {
        get {
            return isActive;
        }

        set {
            isActive = value;
        }
    }

    

    public void SetAction(string value)
    {
        actionValue = value;
    }

    public virtual bool RunAction() {
        return true;
    }
}
