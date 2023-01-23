using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    [SerializeField] int destroyDistance = 20;
    
    public Action<int> OnTakeDamage;
    public Action OnCreepyObjectDestroyed;
    private bool det;

    void OnMessageArrived(string ms)
    {
        Debug.Log("AAAA: " + ms);
        { if (ms == "bang!")
        
            det = true;
            else
            det = false;
        }

    }
    private void Update()
    {
        if (det==true)
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, destroyDistance)) {
                Debug.Log($"hit gameobject {hit.transform.name}");
                CreepyFlyingObject possibleCreepyObject = hit.transform.gameObject.GetComponent<CreepyFlyingObject>();
                if (possibleCreepyObject && possibleCreepyObject.IsAttackingPlayer)
                {
                    Destroy(possibleCreepyObject.gameObject);
                    OnCreepyObjectDestroyed?.Invoke();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals(nameof(CreepyFlyingObject)))
        {
            Destroy(other.gameObject);
            OnTakeDamage?.Invoke(1);
        }
    }
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

}
