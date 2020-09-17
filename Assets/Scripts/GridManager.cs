using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// this class will manage the creation of the 3x3 grid, while also filling each part of the grid with images from the list of images received from the ItemLoader
/// we can change the size of the grid by modifying the variables rows and columns
/// 
/// </summary>
public class GridManager : MonoBehaviour
{

    [SerializeField]
    private int rows = 3;

    [SerializeField]
    private int columns = 3;

    [SerializeField]
    private float tileSize = 15;

    [SerializeField]
    private GameObject ImageTemplate;


    [SerializeField]
    private RectTransform rectTransformCanvas;

    [SerializeField]
    private ItemLoader SceneLoader;

    int count = 0;


    /// <summary>
    /// In the start, we first create the Grid filled with empty images
    /// then we start a coroutine to load each image in the image sprite of the Gameobject corresponding
    /// </summary>

    void Start()
    {
        GenerateGrid();

        foreach (VideoInformation videoInfo in SceneLoader.OutputScript.ClipGroups)
        {
            if (count<9)
            {
                StartCoroutine(DownloadImage(videoInfo.ThumbnailUrl, transform.GetChild(count).gameObject));
                transform.GetChild(count).GetChild(0).GetComponent<Text>().text = videoInfo.Title;

            }
                
            count++;
        }
    }

    /// <summary>
    /// This coroutine will download the image from the url provided and replace the sprite of the image GameObject 
    /// by the downloaded image (adapted into a sprite)
    /// </summary>
    /// <param name="url"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator DownloadImage(string url, GameObject image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            image.GetComponent<Image>().sprite = Sprite.Create(
                DownloadHandlerTexture.GetContent(request), 
                new Rect(0.0f, 0.0f, DownloadHandlerTexture.GetContent(request).width, DownloadHandlerTexture.GetContent(request).height), 
                new Vector2(0.5f, 0.5f));

        }
    }

    /// <summary>
    /// a function Generating automatically a Grid of Images Child of the Canvas where this script is put
    /// it will also change the layer to 8 so that we'll detect the fact that this image is what we want to interact with
    /// </summary>
        private void GenerateGrid()
    {
        GameObject imageSpawn = Instantiate(ImageTemplate);

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject tile = Instantiate(imageSpawn, transform);
                float posX = -rectTransformCanvas.rect.width / 2 + ImageTemplate.GetComponent<RectTransform>().rect.width / 2 + column * (ImageTemplate.GetComponent<RectTransform>().rect.width + tileSize);
                float posY = -rectTransformCanvas.rect.height / 2 + ImageTemplate.GetComponent<RectTransform>().rect.height / 2 + row * (ImageTemplate.GetComponent<RectTransform>().rect.height + tileSize);
                tile.transform.localPosition = new Vector3(posX, posY, 0);
                tile.layer = 8;
            }
        }
        Destroy(imageSpawn);
    }

    
}
