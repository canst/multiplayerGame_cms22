using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    [SerializeField] float destroyRadius = 20f;
    //[SerializeField] int destroyDistance = 20;
    
    public Action<int> OnTakeDamage;
    public Action OnCreepyObjectDestroyed;

    private Animator anim;
    private bool det;

    
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void OnMessageArrived(string ms)
    {
        Debug.Log("AAAA: " + ms);
        {
            if (ms == "bang!")

                det = true;
            else
                det = false;
        }

    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown((int)MouseButton.Left) || det == true)
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
           

            if (Physics.Raycast(ray, out hit))
            {
                anim.Play("Hit",0,0);

                Collider[] hitColliders = Physics.OverlapSphere(hit.point, destroyRadius);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    CreepyFlyingObject possibleCreepyObject = hitColliders[i].GetComponent<CreepyFlyingObject>();
                    if (possibleCreepyObject && possibleCreepyObject.IsAttackingPlayer)
                    {
                        Destroy(possibleCreepyObject.gameObject);
                        OnCreepyObjectDestroyed?.Invoke();
                        break;
                    }
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

    private void HandleAnimation()
    {
        
        
       

    }

    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
