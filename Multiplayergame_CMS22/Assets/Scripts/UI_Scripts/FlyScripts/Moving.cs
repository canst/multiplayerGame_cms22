using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Moving : MonoBehaviour
{
    public GameObject polySurface696_fly;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, polySurface696_fly.transform.position, 10f * Time.deltaTime);
    }
}
