#region Usings

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System;

#endregion

#region ImagesInfo Struct
public struct ImagesInfo
{
    public string name;
    public int width;
    public int height;
    public string path;
    public string extension;

    public static bool operator ==(ImagesInfo img1, ImagesInfo img2)
    {
        return img1.name == img2.name;
    }

    public static bool operator !=(ImagesInfo img1, ImagesInfo img2)
    {
        return img1.name != img2.name;
    }
};

public struct FileInfoStruct
{
    public string name;

    public string path;
    public string extension;
};

public struct Object3DInfo
{
    public string name;
    public string path;
    public string extension;
    public Mesh mesh;

    public List<TextureInfo> texturesInfos;
};

public struct TextureInfo
{
    public string name;
    public string path;
    public Texture2D tex;
    public string ext;
}

#endregion

public class ImageFilesInfoLoader : FilesInfoLoader {


    #region Constructors
    public ImageFilesInfoLoader(string imagesPath) : base(imagesPath)
    {
    }

    public ImageFilesInfoLoader() : base()
    {
    }

    #endregion

    #region Methods

    public List<ImagesInfo> LoadImagesInfo(string [] filesExtensions, string additionalPath = "", int counterLimit = -1, long sizeLimit = -1)
    {
        List<ImagesInfo> imagesInfoList = new List<ImagesInfo>();

        foreach (string extension in filesExtensions)
        {
            string [] imagesNames = Directory.GetFiles(filesPath+additionalPath, "*" + extension);
            
            foreach (string imageName in imagesNames)
            {
                try {
                    Image img = Image.FromFile(imageName);
                    long fileLength = new FileInfo(imageName).Length;

                    if ((sizeLimit > 0 && fileLength < sizeLimit) || sizeLimit == -1)
                    {

                        ImagesInfo imgInfo;
                        imgInfo.name = additionalPath + Path.GetFileName(imageName);
                        imgInfo.width = img.Width;
                        imgInfo.height = img.Height;
                        imgInfo.path = filesPath;
                        imgInfo.extension = extension;
                        imagesInfoList.Add(imgInfo);
                    }
                    else {
                        Debug.Log("Too big shape file");
                    }

                   
                }
                catch (Exception e)
                {
                    Debug.Log("Failed to load: " + imageName);
                    Debug.Log(e);
                }
                if (counterLimit > 0 && imagesInfoList.Count >= counterLimit) break;
            }
            if (counterLimit > 0 && imagesInfoList.Count >= counterLimit) break;
        }

        return imagesInfoList;
    }

    public List<FileInfoStruct> LoadFilesInfo(string[] filesExtensions, string additionalPath = "", int counterLimit = -1, long sizeLimit = -1)
    {
        List<FileInfoStruct> fileInfoList = new List<FileInfoStruct>();

        foreach (string extension in filesExtensions)
        {
            string[] imagesNames = Directory.GetFiles(filesPath + additionalPath, "*" + extension);

            foreach (string imageName in imagesNames)
            {
                try
                {
                    long fileLength = new FileInfo(imageName).Length;

                    if ((sizeLimit > 0 && fileLength < sizeLimit) || sizeLimit == -1)
                    {

                        FileInfoStruct imgInfo;
                        imgInfo.name = additionalPath + Path.GetFileName(imageName);
                        imgInfo.path = filesPath;
                        imgInfo.extension = extension;
                        fileInfoList.Add(imgInfo);
                    }


                }
                catch (Exception e)
                {
                    Debug.Log("Failed to load: " + imageName);
                    Debug.Log(e);
                }
                if (counterLimit > 0 && fileInfoList.Count >= counterLimit) break;
            }
            if (counterLimit > 0 && fileInfoList.Count >= counterLimit) break;
        }

        return fileInfoList;
    }

    public List<Object3DInfo> Load3DObjectsInfos(string[] filesExtensions, string additionalPath = "", int counterLimit = -1, long sizeLimit = -1)
    {
        string[] texturesExtensions = new string[] { ".png", ".jpg"};

        List<Object3DInfo> objectsInfoList = new List<Object3DInfo>();

        foreach (string extension in filesExtensions)
        {
            string[] dirNames = Directory.GetDirectories(filesPath);
            foreach (string dirName in dirNames)
            {
                string[] imagesNames = Directory.GetFiles(dirName + additionalPath, "*" + extension);

                foreach (string imageName in imagesNames)
                {
                    try
                    {
                        long fileLength = new FileInfo(imageName).Length;

                        if ((sizeLimit > 0 && fileLength < sizeLimit*2) || sizeLimit == -1)
                        {

                            Object3DInfo imgInfo;
                            imgInfo.name = additionalPath + Path.GetFileName(imageName);
                            imgInfo.path = dirName;
                            imgInfo.extension = extension;
                            imgInfo.mesh = FastObjImporter.Instance.ImportFile(dirName + "/" + additionalPath + Path.GetFileName(imageName));

                            imgInfo.texturesInfos = new List<TextureInfo>();
                            foreach (string ext in texturesExtensions)
                            {
                                string[] texts = Directory.GetFiles(dirName + additionalPath, "*" + ext);
                                foreach (string tex in texts)
                                {
                                    long fileLength1 = new FileInfo(tex).Length;

                                    if ((sizeLimit > 0 && fileLength1 < sizeLimit) || sizeLimit == -1)
                                    {
                                        TextureInfo texInfo;
                                        texInfo.name = Path.GetFileName(tex);
                                        texInfo.path = dirName;
                                        texInfo.ext = ext;
                                        texInfo.tex = new Texture2D(2, 2);
                                        byte[] bytes = File.ReadAllBytes(dirName + "/" + Path.GetFileName(tex));
                                        texInfo.tex.LoadImage(bytes);
                                        imgInfo.texturesInfos.Add(texInfo);
                                    }
                                    else {
                                        Debug.Log("Too big texture");
                                    }
                                    
                                }

                            }
                                objectsInfoList.Add(imgInfo);
                        }


                    }
                    catch (Exception e)
                    {
                        Debug.Log("Failed to load: " + imageName);
                        Debug.Log(e);
                    }
                    if (counterLimit > 0 && objectsInfoList.Count >= counterLimit) break;
                }
            }

            
            if (counterLimit > 0 && objectsInfoList.Count >= counterLimit) break;
        }

        return objectsInfoList;
    }
    #endregion
}
