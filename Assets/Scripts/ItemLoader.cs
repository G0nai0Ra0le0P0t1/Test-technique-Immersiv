using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// this class will be used to load the data from the json file in a c# variable from a custom class
/// </summary>
public class ItemLoader : MonoBehaviour
{

    
    private ScriptOutput outputScript;

    [SerializeField]
    private string pathToFile = "data.json";

   

    public ScriptOutput OutputScript { get => outputScript; }


    /// <summary>
    /// we load the data in the awake so that we have it available in our start functions
    /// </summary>
    void Awake()
    {
        string stringLoaded = JsonFileReader.LoadJsonFile(pathToFile);
        outputScript = JsonUtility.FromJson<ScriptOutput>(stringLoaded);
    }


}
