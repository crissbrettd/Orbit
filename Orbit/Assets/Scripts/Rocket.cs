using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    public float _LaunchSpeed = 750f;

    [SerializeField]
    public float _MaxDragDistance = 3f;

    private Vector2 _startingPosition;
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
        _rb.isKinematic = true;
        _defaultColor = _spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("down");
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

        float distance = Vector2.Distance(desiredMousePosition, _startingPosition);

        // cannot drag more than certain distance
        if (distance > _MaxDragDistance)
        {
            Vector2 direction = (desiredMousePosition - _startingPosition).normalized;

            desiredMousePosition = (_MaxDragDistance * direction) + _startingPosition;
        }

        // clamp position to left of start
        if (desiredMousePosition.x > _startingPosition.x)
        {
            desiredMousePosition.x = _startingPosition.x;
        }

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
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }
}
