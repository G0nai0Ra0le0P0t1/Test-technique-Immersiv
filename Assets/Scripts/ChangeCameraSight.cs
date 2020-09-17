using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class will handle the rotation of the camera
/// so that the camera follow the cursor
/// </summary>
public class ChangeCameraSight : MonoBehaviour
{
    [SerializeField]
    private float speedRotateX = 2.0f;
    [SerializeField]
    private float speedRotateY = 2.0f;

    private float yRotate = 0f;
    private float xRotate = 0f;

    
    void Update()
    {
        xRotate += speedRotateX * Input.GetAxis("Mouse X");
        yRotate -= speedRotateY * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(yRotate, xRotate, 0f);
    }
}
