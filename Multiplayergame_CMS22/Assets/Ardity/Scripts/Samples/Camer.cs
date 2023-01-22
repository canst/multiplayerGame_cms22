/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class Camer : MonoBehaviour
{
    public GameObject Cam;
    private float rotate;
    public Transform orientation;
    

    private void Update()
    {

        transform.rotation = Quaternion.Euler(0,rotate, 0);
        orientation.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
        rotate = (float.Parse(msg))*2;
        
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
