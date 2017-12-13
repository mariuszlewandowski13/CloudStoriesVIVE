using UnityEngine;


[RequireComponent(typeof(PointableGUIButton))]
public class ActionButton : GUIButton {

    public GUIManager guiManager;

    public string textIfActionExists;
    public string textIfActionNonExists;

    private string _actualText;
    public string actualText
    {
        set {
            _actualText = value;
            UpdateButtonText();
        }
    }

    public virtual void AddAction(string additionalInfo)
    {
        actualText = textIfActionExists + " ("+ additionalInfo + ")";
    }

    public virtual void RemoveAction()
    {
        actualText = textIfActionNonExists;
    }

    private void UpdateButtonText()
    {
        transform.Find("Text").GetComponent<TextMesh>().text = _actualText;
    }   


}
