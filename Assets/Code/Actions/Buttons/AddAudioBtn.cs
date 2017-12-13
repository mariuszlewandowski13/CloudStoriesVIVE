using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointableGUIButton))]
public class AddAudioBtn : MonoBehaviour, IClickable
{

    private string soundToAdd = "";
    public GUIManager manager;

    public void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (manager != null)
        {
            manager.actualSelectedGameObject.GetComponent<ObjectActionsScript>().AddAction(new ObjectAction(true, ObjectActionType.audio, soundToAdd));
            manager.CancelActivateActions();
        }
    }

    public void SetSoundName(string sound, GUIManager man)
    {
        soundToAdd = sound;
        manager = man;
        ChangeSceneNumberInfoShowing();
    }

    private void ChangeSceneNumberInfoShowing()
    {
        GetComponent<TextMesh>().text = soundToAdd;
        gameObject.AddComponent<BoxCollider>();
    }
}
