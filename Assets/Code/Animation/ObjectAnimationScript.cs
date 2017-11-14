using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationScript : MonoBehaviour {

    private List<ObjectAnimationState> objectStates;

    private int actualIndexWhilePlaying;

    public bool isActive;

    public int maxFrame = 0;

    private void Start()
    {
        objectStates = new List<ObjectAnimationState>();
        isActive = true;
        if (GameObject.Find("AnimationManager") != null)
        {
            GameObject.Find("AnimationManager").GetComponent<AnimationManager>().AddAnimatedObject(this);

        }

    }

    public void InitRecording()
    {
        objectStates.Clear();
    }

    public void SaveNextState(int frame)
    {
        ObjectAnimationState state = new ObjectAnimationState(isActive, transform.position, transform.rotation.eulerAngles, transform.lossyScale, frame);
        objectStates.Add(state);
        maxFrame = frame;
    }

    public void InitPlaying()
    {
        actualIndexWhilePlaying = 0;
    }

    public void PlayNextState()
    {
        if (actualIndexWhilePlaying < objectStates.Count)
        {
            ObjectAnimationState state = objectStates[actualIndexWhilePlaying];

            if (state.isActive && !gameObject.activeSelf) gameObject.SetActive(true);
            if (!state.isActive && gameObject.activeSelf) gameObject.SetActive(false);

            transform.position = state.position;
            transform.rotation = Quaternion.Euler(state.rotation);

            //Transform prevParent = transform.parent;

            //transform.parent = null;
            //transform.localScale = state.scale;

            //transform.parent = prevParent;
            actualIndexWhilePlaying++;
        }
    }

    public void SetInitialState()
    {
        if ( objectStates.Count > 0)
        {
            ObjectAnimationState state = objectStates[0];

            gameObject.SetActive(true);

            transform.position = state.position;
            transform.rotation = Quaternion.Euler(state.rotation);

            //Transform prevParent = transform.parent;

            //transform.parent = null;
            //transform.localScale = state.scale;

            //transform.parent = prevParent;
        }
    }

    private void OnDestroy()
    {
        if (GameObject.Find("AnimationManager") != null)
        {
            GameObject.Find("AnimationManager").GetComponent<AnimationManager>().RemoveAnimatedObject(this);

        }
    }
}
