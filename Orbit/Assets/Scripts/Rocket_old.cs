using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI speedTMP;

    [SerializeField]
    public TextMeshProUGUI accelerationTMP;

    [Space]

    [SerializeField]
    public float _baseLaunchSpeed = 100f;

    [SerializeField]
    public float _maxDragDistance = 10f;

    [SerializeField]
    public float _lineWidth = 0.1f;

    [SerializeField]
    public float _lineLength = 10f;

    public float _currentGravitationalAcceleration = 0f;

    private Vector2 _startingPosition;
    private Quaternion _startingRotation;
    private Vector2 _direction;
    private float distance;
    private float dragLaunchSpeedMultiplier;

    // For use in speed calculations
    private Vector2 _lastPosition = Vector2.zero;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    private LineRenderer line;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
        _rb.isKinematic = true;
        //_defaultColor = _spriteRenderer.color;

        line = gameObject.AddComponent<LineRenderer>();
        line.startColor = Color.grey;
        line.endColor = Color.grey;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 distancePerFrame = transform.position - (Vector3)_lastPosition;
        _lastPosition = transform.position;
        Vector3 speed = distancePerFrame * Time.deltaTime;

        Debug.Log(speed);
        accelerationTMP.text = $"Gravitational Acceleration: {_currentGravitationalAcceleration}";
    }

    void LateUpdate()
    {
        // Only move transform if rocket is moving
        if (_rb.isKinematic == false)
        {
            transform.up = _rb.velocity.normalized;
        }
    }

    void OnMouseDown()
    {
        //_spriteRenderer.color = Color.green;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = transform.position;
        Vector2 direction = (_startingPosition - currentPosition).normalized;

        dragLaunchSpeedMultiplier = distance <= _maxDragDistance ? distance : _maxDragDistance;

        float modifiedLaunchSpeed = dragLaunchSpeedMultiplier * _baseLaunchSpeed;

        speedTMP.text = $"Initial Speed: {modifiedLaunchSpeed}";

        _rb.isKinematic = false;
        _rb.AddForce(direction * modifiedLaunchSpeed);

        //_spriteRenderer.color = _defaultColor;

        line.enabled = false;
    }

    void OnMouseDrag()
    {
        Vector2 desiredMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (desiredMousePosition - _startingPosition).normalized;
        distance = Vector2.Distance(desiredMousePosition, _startingPosition);

        Debug.Log(distance);

        //dragLaunchSpeedMultiplier = distance % 10

        // cannot drag more than certain distance
        if (distance > _maxDragDistance)
        {
            desiredMousePosition = (_maxDragDistance * _direction) + _startingPosition;
        }

        _rb.position = desiredMousePosition;

        DrawLine();
    }

    void DrawLine()
    {
        List<Vector3> pos = new List<Vector3>();
        pos.Add(new Vector3(transform.position.x, transform.position.y));
        pos.Add(new Vector3(transform.position.x + _lineLength * -_direction.x, transform.position.y + _lineLength * -_direction.y));
        line.startWidth = _lineWidth;
        line.endWidth = _lineWidth;
        line.SetPositions(pos.ToArray());
        line.useWorldSpace = true;
        line.enabled = true;
    }
}
