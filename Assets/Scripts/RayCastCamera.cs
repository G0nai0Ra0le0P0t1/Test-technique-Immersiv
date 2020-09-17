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

    [SerializeField]
    private float timerReset=3.0f;
    private float timer;


    [SerializeField]
    private Image imageFromScreen;

    [SerializeField]
    private Texture2D cursorImage;

    void Start()
    {
        timer = timerReset;
    }

    /// <summary>
    /// in the update, we check that our camera is looking at one of the target images.
    /// If so, we start/continue the timer
    /// Once the timer is over, we show this "video" on the Screen
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
                            if (objectHit.gameObject.GetComponent<Image>()!=null)
                                imageFromScreen.sprite = objectHit.gameObject.GetComponent<Image>().sprite;
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
