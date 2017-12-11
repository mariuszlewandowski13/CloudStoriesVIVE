using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSpawningButton : Object3DSpawningButton
{

    public string path2;


    public override void Clicked(Vector3 pos, GameObject clickingObject)
    {
        if (GetComponent<MediaPlayerCtrl>().m_strFileName != "" && GetComponent<MediaPlayerCtrl>().GetCurrentState() != MediaPlayerCtrl.MEDIAPLAYER_STATE.ERROR)
        {
            clickingObject.GetComponent<RaycastObjectSpawner>().StartSpawning(objectToSpawn, pos, transform.lossyScale, transform.rotation.eulerAngles, ObjectsTypes.movie, path2, basePath, "MP4", true);
        }
    }

    public void SetObjectTypes(string path, string path2)
    {
        basePath = path;
        this.path2 = path2;
        LoadVideo();
    }

    public void LoadVideo()
    {
        if (path2 != "" && GetComponent<MediaPlayerCtrl>() != null)
        {
            GetComponent<MediaPlayerCtrl>().m_strFileName = basePath + path2;
        }
    }

    
}
