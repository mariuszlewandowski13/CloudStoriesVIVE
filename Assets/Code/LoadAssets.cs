using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadAssets : MonoBehaviour {

    private void Start()
    {
        Load2DShapes();
       // LoadMovies();
       // LoadGIFS();
        Load3DObjects();
        LoadAudio();
       // Print3DObjectsInfo();
    }


    private void Load2DShapes()
    {
        ApplicationStaticData.shapesInfos = new List<ImagesInfo>();
            try
            {
                ImageFilesInfoLoader loader = new ImageFilesInfoLoader(ApplicationStaticData.shapesPath);
                string[] extensions = {".png" };
            ApplicationStaticData.shapesInfos = loader.LoadImagesInfo(extensions, "", -1, 5000000);
            }
            catch (Exception e)
            {
                Debug.Log("Unable to load images!");
                Debug.Log(e);
            }
    }

    private void LoadMovies()
    {
        ApplicationStaticData.moviesInfos = new List<FileInfoStruct>();
        try
        {
            ImageFilesInfoLoader loader = new ImageFilesInfoLoader(ApplicationStaticData.moviesPath);
            string[] extensions = { ".mp4" };
            ApplicationStaticData.moviesInfos = loader.LoadFilesInfo(extensions);
        }
        catch (Exception e)
        {
            Debug.Log("Unable to load movies!");
            Debug.Log(e);
        }
    }

    private void LoadGIFS()
    {
        ApplicationStaticData.gifsInfos = new List<FileInfoStruct>();
        try
        {
            ImageFilesInfoLoader loader = new ImageFilesInfoLoader(ApplicationStaticData.gifsPath);
            string[] extensions = { ".mp4" };
            ApplicationStaticData.gifsInfos = loader.LoadFilesInfo(extensions);
        }
        catch (Exception e)
        {
            Debug.Log("Unable to load gifs!");
            Debug.Log(e);
        }
    }

    private void Load3DObjects()
    {
        ApplicationStaticData.objects3DInfos = new List<Object3DInfo>();
        try
        {
            ImageFilesInfoLoader loader = new ImageFilesInfoLoader(ApplicationStaticData.objects3DPath);
            string[] extensions = { ".obj" };
            ApplicationStaticData.objects3DInfos = loader.Load3DObjectsInfos(extensions, "", -1, 5000000);
        }
        catch (Exception e)
        {
            Debug.Log("Unable to load 3D Objects!");
            Debug.Log(e);
        }
    }

    private void LoadAudio()
    {
        ApplicationStaticData.audioInfos = new List<FileInfoStruct>();
        try
        {
            ImageFilesInfoLoader loader = new ImageFilesInfoLoader(ApplicationStaticData.audioPath);
            string[] extensions = { ".mp3" };
            ApplicationStaticData.audioInfos = loader.LoadFilesInfo(extensions);
        }
        catch (Exception e)
        {
            Debug.Log("Unable to load mp3!");
            Debug.Log(e);
        }
    }

    private void Print3DObjectsInfo()
    {
        foreach (Object3DInfo info in ApplicationStaticData.objects3DInfos)
        {
            Debug.Log("3DObject: " + info.name + " " + info.path);
            foreach (TextureInfo texInfo in info.texturesInfos)
            {
                Debug.Log("Texture: " + texInfo.name + " " + texInfo.path);

            }
        }
    }

}
