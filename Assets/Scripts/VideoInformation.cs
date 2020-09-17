using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// a class that will be used when we will load the information from the json
/// </summary>
[System.Serializable]
public class VideoInformation
{
    public string ID;
    public string Url;
    public string Title;
    public string Phase;
    public int minute;
    public string[] category;
    public string ThumbnailUrl;
}
