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
    private GameObject ImageTemplate=null;

    [SerializeField]
    private RectTransform RectTemplate = null;

    [SerializeField]
    private RectTransform rectTransformCanvas=null;

    [SerializeField]
    private ItemLoader SceneLoader=null;

    int count = 0;


    /// <summary>
    /// In the start, we first create the Grid filled with empty images
    /// then we start a coroutine to load each image in the image sprite of the Gameobject corresponding
    /// while resizing the images to the correct dimensions while keeping the same ratio
    /// to avoid deforming the images
    /// </summary>

    void Start()
    {
        GenerateGrid();
    }

    /// <summary>
    /// This coroutine will download the image from the url provided and replace the sprite of the image 
    /// using the GetRectTransform() method to get the rectTransform of the image and resizing it
    /// and changing it's sprite, using the SetSprite() method,
    /// by the downloaded image (adapted into a sprite)
    /// </summary>
    /// <param name="url"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator DownloadImage(string url, GetInformationFromCanvas image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            reziseCanvas(image.GetRectTransform(), DownloadHandlerTexture.GetContent(request).width, DownloadHandlerTexture.GetContent(request).height);
            image.SetSprite(Sprite.Create(
                DownloadHandlerTexture.GetContent(request), 
                new Rect(0.0f, 0.0f, DownloadHandlerTexture.GetContent(request).width, DownloadHandlerTexture.GetContent(request).height), 
                new Vector2(0.5f, 0.5f)));

        }
    }

    /// <summary>
    /// a function that resize the canva to the correct width and height
    /// while keeping the same ration between it's width and height
    /// </summary>
    /// <param name="canva"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    private void reziseCanvas(RectTransform canva, float width, float height)
    {
        if (width > height)
        {
            canva.sizeDelta = new Vector2(canva.rect.width, canva.rect.width * height / width);
        }
        else
        {
            canva.sizeDelta = new Vector2(width * canva.rect.height / height, canva.rect.height);
        }

    }

    /// <summary>
    /// a function Generating automatically a Grid of Images Child of the Canvas where this script is put
    /// it will also change the layer to 8 so that we'll detect the fact that this image is what we want to interact with
    /// </summary>
        private void GenerateGrid()
    {
        GameObject imageSpawn = Instantiate(ImageTemplate);
        GetInformationFromCanvas getInfoFromCanvas = null;
        int row = 0;
        int column = 0;
        foreach (VideoInformation videoInfo in SceneLoader.OutputScript.ClipGroups)
        {
            if (count < rows* columns)
            {
                GameObject tile = Instantiate(imageSpawn, transform);
                getInfoFromCanvas = tile.transform.GetComponent<GetInformationFromCanvas>();
                float posX = -rectTransformCanvas.rect.width / 2 + RectTemplate.rect.width / 2 + column * (RectTemplate.rect.width + tileSize);
                float posY = -rectTransformCanvas.rect.height / 2 + RectTemplate.rect.height / 2 + row * (RectTemplate.rect.height + tileSize);
                tile.transform.localPosition = new Vector3(posX, posY, 0);
                tile.layer = 8;
                column++;
                if (column == columns)
                {
                    column -= columns;
                    row++;
                }

                
                StartCoroutine(DownloadImage(videoInfo.ThumbnailUrl, getInfoFromCanvas));
                //tile.transform.GetChild(0).GetComponent<Text>().text = videoInfo.Title;
                getInfoFromCanvas.SetText(videoInfo.Title);

            }

            count++;
        }
        Destroy(imageSpawn);
    }

    
}
