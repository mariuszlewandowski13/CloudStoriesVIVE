using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimelineScript : MonoBehaviour {
    private class TimelineAnimatedObjectsData
    {
        public GameObject animatedObjectTimeline;
        public ObjectAnimationScript animationScript;

        

        public TimelineAnimatedObjectsData(ObjectAnimationScript scr, GameObject obj)
        {
            animationScript = scr;
            animatedObjectTimeline = obj;
        }
    }



    public GameObject timelineBackground;
    public GameObject timelineObjectPrefab;


    private List<TimelineAnimatedObjectsData> animatedObjectsTimeLines;

    private float backgroundYScale;
    private float posYChange;
    private Vector3 startPosition;

    private void Start()
    {
        animatedObjectsTimeLines = new List<TimelineAnimatedObjectsData>();
    }



    public void AddAnimatedObject(ObjectAnimationScript animationScript)
    {
        GameObject newTimeline = Instantiate(timelineObjectPrefab);
        newTimeline.transform.Find("ObjectName").GetComponent<TextMesh>().text = animationScript.gameObject.name;
        animatedObjectsTimeLines.Add(new TimelineAnimatedObjectsData(animationScript, newTimeline));
        UpdateTimeLinePresention();

    }

    public void RemoveAnimatedObject(ObjectAnimationScript animationScript)
    {
        TimelineAnimatedObjectsData objToDestroy = null;

        foreach (TimelineAnimatedObjectsData data in animatedObjectsTimeLines)
        {
            if (animationScript == data.animationScript)
            {
                objToDestroy = data;
                break;
            }
        }

        if (objToDestroy != null)
        {
            animatedObjectsTimeLines.Remove(objToDestroy);
            Destroy(objToDestroy.animatedObjectTimeline);
            UpdateTimeLinePresention();
        }
    }


    public void UpdateTimeLinePresention()
    {
        CalculateTimeLineOffsets();
        ShowTimeLines();
    }

    private void CalculateTimeLineOffsets()
    {
         backgroundYScale = timelineBackground.transform.lossyScale.y;
         posYChange = backgroundYScale / animatedObjectsTimeLines.Count;

         startPosition = timelineBackground.transform.position;
        startPosition.y += backgroundYScale / 2.0f;
        startPosition.y -= posYChange / 2.0f;
    }

    private void ShowTimeLines()
    {
        Vector3 scale = timelineObjectPrefab.transform.localScale;
        scale.y = scale.y / animatedObjectsTimeLines.Count;

        foreach (TimelineAnimatedObjectsData data in animatedObjectsTimeLines)
        {
            data.animatedObjectTimeline.transform.position = startPosition;
            startPosition.y -= posYChange;

            data.animatedObjectTimeline.transform.localScale = scale;
        }
    }


}
