using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class is the class that will handle the interaction between the camera and the different videos
/// </summary>
public class RayCastCamera : MonoBehaviour
{
    public new Camera camera;
    RaycastHit hit;
    Transform objectHit;

    /// <summary>
    /// the timer that we'll use for the reset
    /// </summary>
    [SerializeField]
    private float timerReset=3.0f;
    private float timer;

    /// <summary>
    /// the image of our screen 
    /// </summary>
    [SerializeField]
    private Image imageFromScreen=null;


    /// <summary>
    /// the Image used as our cursor 
    /// </summary>
    [SerializeField]
    private Texture2D cursorImage=null;

    //the image Hit with the raycast 
    private Image hitImage;

    //the Initial Height of our screen 
    private float initialHeight;

    //the Initial Width of our screen 
    private float initialWidth;

    /// <summary>
    /// in the start, we'll save both the height and the width initial of our sceen
    /// and initialize the timer
    /// </summary>
    void Start()
    {
        timer = timerReset;
        initialWidth = imageFromScreen.rectTransform.rect.width;
        initialHeight = imageFromScreen.rectTransform.rect.height;
    }

    /// <summary>
    /// in the update, we check that our camera is looking at one of the target images.
    /// If so, we start/continue the timer
    /// Once the timer is over, we show this "video" on the Screen that we resize to the proper size
    /// </summary>
    void Update()
    {
        Ray raycast = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(raycast, out hit))
        {
            if (objectHit != null)
            {
                if (objectHit.gameObject.layer == 8)
                {
                    if (objectHit.gameObject == hit.transform.gameObject)
                    {
                        if (timercount())
                        {
                            if ((hitImage = objectHit.gameObject.GetComponent<Image>()) != null)
                            {
                                if (hitImage.rectTransform.rect.width > hitImage.rectTransform.rect.height)
                                {
                                    imageFromScreen.rectTransform.sizeDelta = 
                                        new Vector2(initialWidth, initialWidth * hitImage.rectTransform.rect.height / hitImage.rectTransform.rect.width);
                                }
                                else
                                {
                                    imageFromScreen.rectTransform.sizeDelta = 
                                        new Vector2(hitImage.rectTransform.rect.width * initialHeight / hitImage.rectTransform.rect.height, initialHeight);
                                }
                                imageFromScreen.sprite = hitImage.sprite;
                                
                            }
                                
                        }
                    }
                    else
                    {
                        objectHit = hit.transform;
                        ResetTimer();
                    }
                }
            
            }
            else
            {

                objectHit = hit.transform;
                ResetTimer();
            }



        }
    }

    /// <summary>
    /// this script handle the cursor that we see beside our mouse cursor,
    /// showing us where we're looking at in the scene
    /// </summary>
    void OnGUI()
    {
        Vector3 mPos = Input.mousePosition;
        GUI.DrawTexture(new Rect(mPos.x - 32, Screen.height - mPos.y - 32, 64, 64), cursorImage);
    }

    public void ResetTimer()
    {
        timer = timerReset;
    }


    public bool timercount()
    {
        if (timer < 0)
            return true;
        timer -= Time.deltaTime;
        return false;
    }



}
