using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LoadingTest : MonoBehaviour {
    float emptySize = 1.0f;
    bool loading;

    public GameObject object3DPrefab;

	// Use this for initialization
	void Start () {
        //Mesh mesh = FastObjImporter.Instance.ImportFile("C:/media/Objects3D/Mesh_Ape.obj");
        //GetComponent<MeshFilter>().mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
        if (!loading)
        {

            LoadObjects();
            loading = true;
        }
	}

    void LoadObjects()
    {
        GameObject newObject = null;
        foreach (Object3DInfo info in ApplicationStaticData.objects3DInfos)
        {
             newObject = Instantiate(object3DPrefab);
            try
            {
                Mesh mesh = FastObjImporter.Instance.ImportFile(info.path + "/" + info.name);
                newObject.GetComponent<MeshFilter>().mesh = mesh;

                var mf = newObject.GetComponent<MeshFilter>();

                if (mf != null)
                {
                    var bounds = mf.mesh.bounds;

                    float max = bounds.extents.x;
                    if (max < bounds.extents.y)
                        max = bounds.extents.y;
                    if (max < bounds.extents.z)
                        max = bounds.extents.z;
                    if (max != 0.0f)
                    {
                        float scale = (emptySize * 0.5f) / max;

                        newObject.transform.localScale = new Vector3(scale, scale, scale);
                    }
                        
                }

                if (info.texturesInfos.Count > 0)
                {
                    Texture2D tex = new Texture2D(2, 2);
                    byte[] bytes = File.ReadAllBytes(info.texturesInfos[0].path + "/" + info.texturesInfos[0].name);

                    tex.LoadImage(bytes);
                    newObject.GetComponent<Renderer>().material.mainTexture = tex;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            
        }
    }
}
