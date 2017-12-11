using UnityEngine;

public class ActionButton : GUIButton {

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

    public virtual void AddAction()
    {
        actualText = textIfActionExists;
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
