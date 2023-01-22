using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MessageListener : MonoBehaviour
{
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
        //gameObject.SendMessage("PlayerCam", 1);
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    float anglee;
    string msg;

    //string OnMessageArrived = float.OnMessageArrived;


private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
  
    }
        private void Update()
    {
       
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
    
        yRotation = anglee;

        xRotation = anglee;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}