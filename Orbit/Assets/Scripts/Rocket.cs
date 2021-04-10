using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    public float _LaunchSpeed = 750f;

    [SerializeField]
    public float _MaxDragDistance = 10f;

    private Vector2 _startingPosition;
    private Quaternion _startingRotation;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

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
            transform.right = _rb.velocity.normalized;
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
    }

    void OnMouseDrag()
    {
        Vector2 desiredMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = (desiredMousePosition - _startingPosition).normalized;

        //float angle = Vector2.Angle(desiredMousePosition, direction);

        float distance = Vector2.Distance(desiredMousePosition, _startingPosition);

        //Debug.Log(angle);

        // cannot drag more than certain distance
        if (distance > _MaxDragDistance)
        {
            desiredMousePosition = (_MaxDragDistance * direction) + _startingPosition;
        }
        
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

        _rb.position = desiredMousePosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        transform.position = _startingPosition;
        transform.rotation = _startingRotation;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }
}
