using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 120f;
    public float mouseSpeed = 4f;
    public Transform playerBody;

    float xRotation;


   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        //old input system float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

       float mouseX = 0;
       float mouseY = 0;


        if (Mouse.current != null)
        {
            mouseX = Mouse.current.delta.ReadValue().x * mouseSpeed;
            mouseY = Mouse.current.delta.ReadValue().y * mouseSpeed;
        }



        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);



        playerBody.Rotate(Vector3.up * mouseX * Time.deltaTime);



    }
}
