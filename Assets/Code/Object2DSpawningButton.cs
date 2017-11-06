using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Object2DSpawningButton : Object3DSpawningButton {

    public Texture2D tex;

    public string path2;

    private float ratio;

    public override void Clicked(Vector3 pos, GameObject clickingObject)
    {
        clickingObject.GetComponent<RaycastObjectSpawner>().StartSpawning(objectToSpawn, pos,transform.lossyScale, transform.rotation.eulerAngles, ObjectsTypes.shapeObject, path2, basePath, "PNG");

    }

    public void SetObjectTypes(string path, string path2, float ratio)
    {
        this.basePath = path;
        this.path2 = path2;
        this.ratio = ratio;
        LoadTexture();
    }

    public void LoadTexture()
    {
        if (path2 != "")
        {
            byte [] bytes = bytes = File.ReadAllBytes(basePath + path2);
            tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            GetComponent<Renderer>().material.mainTexture = tex;
            ChangeSizeByRatio();
        }
    }

    private void ChangeSizeByRatio()
    {
        if (ratio != 0.0f)
        {
            

            Vector3 scale = transform.localScale;
            if (ratio < 1.0f)
            {
                scale.z *= ratio;
            }
            else {
                scale.x *= (1/ratio);
            }
            
            transform.localScale = scale;
        }
    }
}
