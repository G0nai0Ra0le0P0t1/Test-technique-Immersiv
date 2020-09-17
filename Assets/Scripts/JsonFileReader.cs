using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// a class that will be used to load the json file
/// </summary>
public class JsonFileReader : MonoBehaviour
{
    public static string LoadJsonFile(string path)
    {
        string jsonFilePath = path.Replace(".json", "");
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFilePath);
        return jsonFile.text;
    }

}
