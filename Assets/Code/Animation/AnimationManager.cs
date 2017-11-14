using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationState
{ 

    public bool isActive;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public int frame;

    public ObjectAnimationState(bool active, Vector3 pos, Vector3 rot, Vector3 sca, int frame)
    {
        isActive = active;
        position = pos;
        rotation = rot;
        scale = sca;
        this.frame = frame;
    }
}


public class AnimationManager : MonoBehaviour {


    public AnimGUIButton playButton;
    public AnimGUIButton pauseButton;
    public AnimGUIButton stopButton;
    public AnimGUIButton recordButton;

    public ObjectAnimationScript actualObject;

    public TimelineScript timeline;


    public List<ObjectAnimationScript> animatedObjects;

    public static bool isRecording;
    public static bool isPlaying;

    public static bool looping = true;

    public static bool isWorking
    {
        get
        {
            return isRecording || isPlaying;
        } 
    }

    public static bool isPaused;

    public int maxFramesCount = 0;
    public int framesCount = 0;
    public int actualframesCount = 0;

    public bool actualObjectIsToRemove = false;



    private void Awake()
    {
        animatedObjects = new List<ObjectAnimationScript>();
        playButton.DeactivateObject();
        recordButton.DeactivateObject();
        pauseButton.DeactivateObject();
        stopButton.DeactivateObject();
    }

    public void SetActualAnimatedObject(ObjectAnimationScript newObj)
    {
        if (actualObject == null)
        {
            actualObject = newObj;
            recordButton.ActivateObject();
            GameObject.Find("GUI").transform.Find("Right Panel").Find("ObjectsAnimationButtons").GetComponent<AnimationObjectActivationManager>().SetActivationObject(newObj);
        }
    }

    public void RemoveActualAnimatedObject()
    {
        if (actualObject != null)
        {
            if (!isRecording)
            {
                actualObject = null;
                recordButton.DeactivateObject();
                GameObject.Find("GUI").transform.Find("Right Panel").Find("ObjectsAnimationButtons").GetComponent<AnimationObjectActivationManager>().DeleteActivationObject();
            }
            else
            {
                actualObjectIsToRemove = true;
            }
            
        }
    }

    public void StartRecording()
    {
        if (actualObject != null)
        {
            playButton.DeactivateObject();
            recordButton.DeactivateObject();
            pauseButton.ActivateObject();
            stopButton.ActivateObject();

            isRecording = true;
            framesCount = 0;
            actualObject.InitRecording();
        }
           
        
    }

    public void StartPlaying()
    {
        playButton.DeactivateObject();
        recordButton.DeactivateObject();
        pauseButton.ActivateObject();
        stopButton.ActivateObject();

        InitPlaying();
    }

    private void InitPlaying()
    {
        isPlaying = true;
        actualframesCount = 0;
        foreach (ObjectAnimationScript animObj in animatedObjects)
        {
            animObj.InitPlaying();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }

    private void Update()
    {
        if (isRecording && !isPaused)
        {
            actualObject.SaveNextState(framesCount);
            framesCount++;
        }

        if (isPlaying && !isPaused)
        {
            foreach (ObjectAnimationScript animObj in animatedObjects)
            {
                animObj.PlayNextState();
            }
            actualframesCount++;
            if (actualframesCount >= maxFramesCount)
            {
                if (looping)
                {
                    InitPlaying();
                }
                else {
                    StopPlaying();
                }
                
            }
        }
    }


    public void Stop()
    {
        if (isRecording)
        {
            StopRecording();
        }
        else if (isPlaying)
        {
            StopPlaying();
        }
    }

    public void StopRecording()
    {
        playButton.ActivateObject();
        recordButton.ActivateObject();
        pauseButton.DeactivateObject();
        stopButton.DeactivateObject();

        SetInitialStateForObjects();
        isRecording = false;
        UpdateMaxFramesCount();

        timeline.AddAnimatedObject(actualObject);

        if (actualObjectIsToRemove)
        {
            actualObjectIsToRemove = false;
            RemoveActualAnimatedObject();
        }

    }

    private void UpdateMaxFramesCount()
    {
        maxFramesCount = 0;
        foreach (ObjectAnimationScript animObj in animatedObjects)
        {
            if (animObj.maxFrame > maxFramesCount) maxFramesCount = animObj.maxFrame;
        }
    }

    public void StopPlaying()
    {
        playButton.ActivateObject();
        recordButton.ActivateObject();
        pauseButton.DeactivateObject();
        stopButton.DeactivateObject();

        SetInitialStateForObjects();
        isPlaying = false;
    }

    private void SetInitialStateForObjects()
    {
        foreach (ObjectAnimationScript animObj in animatedObjects)
        {
            animObj.SetInitialState();
        }
    }

    public void AddAnimatedObject(ObjectAnimationScript newObj)
    {
        animatedObjects.Add(newObj);
    }

    public void RemoveAnimatedObject(ObjectAnimationScript newObj)
    {
        animatedObjects.Remove(newObj);
        timeline.RemoveAnimatedObject(newObj);
    }




}
