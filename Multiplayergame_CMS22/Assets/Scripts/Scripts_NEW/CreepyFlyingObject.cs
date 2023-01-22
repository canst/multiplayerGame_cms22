using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CreepyFlyingObject : MonoBehaviour
{
    
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] int delayUntilMovementStarts = 4;
        
    [Header("Movement")]
    [SerializeField] int timeToFly = 4;
    [SerializeField] Transform targetTransform;
    

    private bool _activated;
    private float _currentTimeActivated = 0.0f;
    
    private bool _startFlying;
    private float _currentTimeMove = 0.0f;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private bool _shouldPlaySound;
    
    public bool IsAttackingPlayer { get; set; }

    private void Awake()
    {
        gameObject.tag = this.GetType().ToString();
        gameObject.layer = LayerMask.NameToLayer(this.GetType().ToString());
        GetComponent<MeshCollider>().convex = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_activated)
        {
            // play sound once, then start countdown until object starts flying
            if (!_shouldPlaySound && audioSource)
            {
                audioSource.Play();
                _shouldPlaySound = true;
            }
            _currentTimeActivated += Time.deltaTime;
            if (_currentTimeActivated >= delayUntilMovementStarts)
            {
                _startFlying = true;
                _activated = false;
                IsAttackingPlayer = true;
            }
        }
        
        if (_startFlying)
        {
            _currentTimeMove += Time.deltaTime/timeToFly;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _currentTimeMove);
        }
    }

    public void Activate()
    {
        _activated = true;
        targetTransform = FindObjectOfType<Player>().transform;
        _targetPosition = targetTransform.position - (transform.parent.position - transform.position);
    }
    
}
