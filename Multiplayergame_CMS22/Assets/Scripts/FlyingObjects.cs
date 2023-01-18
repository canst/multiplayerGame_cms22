using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjects : ChassingElements
{
    public float horizontalSpeed = 0.001f;
    public float verticalSpeed = 2.0f;
    public float height = 2.5f;

    public Vector3 tempPosition;
    public Transform[] waypoints;

    private int waypointIndex = 0;

    protected override Vector3 GetNextDestination()
    {
        Vector3 destination = waypoints[waypointIndex].position;

        waypointIndex = (waypointIndex + 1) % waypoints.Length;

        return destination;
    }
    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * height;
        transform.position = tempPosition;
        
    }
}
