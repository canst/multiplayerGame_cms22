using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

using static UnityEngine.AnimationClip;

public class Player : MonoBehaviour
{

    [SerializeField] int destroyDistance = 20;
    
    public Action<int> OnTakeDamage;
    public Action OnCreepyObjectDestroyed;

    //adding hand aninmation script

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void AnimateHand()
    {
        animator.SetTrigger("HandAnimation");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int) MouseButton.Left))
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

}
