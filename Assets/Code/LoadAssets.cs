using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadAssets : MonoBehaviour {

    private void Start()
    {
        Load2DShapes();
    }


    private void Load2DShapes()
    {
        ApplicationStaticData.shapesInfos = new List<ImagesInfo>();
            try
            {
                ImageFilesInfoLoader loader = new ImageFilesInfoLoader(ApplicationStaticData.shapesPath);
                string[] extensions = {".png" };
            ApplicationStaticData.shapesInfos = loader.LoadImagesInfo(extensions);
            }
            catch (Exception e)
            {
                Debug.Log("Unable to load images!");
                Debug.Log(e);
            }
    }

}
