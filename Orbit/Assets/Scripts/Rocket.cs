using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    public float _LaunchSpeed = 750f;

    [SerializeField]
    public float _MaxDragDistance = 10f;

    [SerializeField]
    public float _LineWidth = 1f;

    [SerializeField]
    public float _LineLength = 10f;

    private Vector2 _startingPosition;
    private Quaternion _startingRotation;
    private Vector2 _direction;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    private LineRenderer line;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
        _rb.isKinematic = true;
        _defaultColor = _spriteRenderer.color;

        line = gameObject.AddComponent<LineRenderer>();
        line.startColor = Color.white;
        line.endColor = Color.white;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ResetAfterDelay();
        //}
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
        _spriteRenderer.color = Color.green;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = transform.position;
        Vector2 direction = (_startingPosition - currentPosition).normalized;

        _rb.isKinematic = false;
        _rb.AddForce(direction * _LaunchSpeed);

        _spriteRenderer.color = _defaultColor;

        line.enabled = false;
    }

    void OnMouseDrag()
    {
        Vector2 desiredMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (desiredMousePosition - _startingPosition).normalized;
        float distance = Vector2.Distance(desiredMousePosition, _startingPosition);

        // cannot drag more than certain distance
        if (distance > _MaxDragDistance)
        {
            desiredMousePosition = (_MaxDragDistance * _direction) + _startingPosition;
        }

        _rb.position = desiredMousePosition;

        DrawLine();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        transform.position = _startingPosition;
        transform.rotation = _startingRotation;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }

    void DrawLine()
    {
        List<Vector3> pos = new List<Vector3>();
        pos.Add(new Vector3(transform.position.x, transform.position.y));
        pos.Add(new Vector3(transform.position.x + _LineLength * -_direction.x, transform.position.y + _LineLength * -_direction.y));
        line.startWidth = _LineWidth;
        line.endWidth = _LineWidth;
        line.SetPositions(pos.ToArray());
        line.useWorldSpace = true;
        line.enabled = true;
    }
}
