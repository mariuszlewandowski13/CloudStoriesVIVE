#region Usings
using System.Collections.Generic;
using System;
using UnityEngine;


#endregion

public static class ApplicationStaticData
{

    #region Public Properties


    static public Color objectsHighligthing = new Color(1f, 0f, 0f, 0.1f);
    static public Color objectsHighligthingNormalColor = new Color(1f, 1f, 1f, 0.1f);

    public static string serverScriptsPath = "http://vrowser.e-kei.pl/CloudStories/";
    public static int owner = 0;

    public static List<int> layoutsNumber;

    public static ProjectObject actualProject;

    public static List<ImagesInfo> shapesInfos;


    public static string mediaPath = "C:/media/";
    public static string shapesPath = mediaPath + "Shapes/";

    #endregion


}