using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rocketv2 : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI speedTMP;

    [SerializeField]
    TextMeshProUGUI accelerationTMP;

    [SerializeField]
    Slider LaunchSpeedSlider;

    [SerializeField][Range(0, 100)]
    private float launchSpeed = 0f;

    private bool hasRocketLaunched;

    private Vector2 startingPosition;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.isKinematic = true;
        startingPosition = transform.position;
        hasRocketLaunched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRocketLaunched) {
            if (Input.GetKey(KeyCode.Space)) {
                AddSpeedToLaunch();            
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                LaunchRocket();
            }
        }
    }

    void LateUpdate()
    {
        // Only move transform if rocket is moving
        if (_rb.isKinematic == false)
        {
            transform.up = _rb.velocity.normalized;
        }
    }

    void AddSpeedToLaunch() {
        launchSpeed += 2f;
        speedTMP.text = $"Initial Speed: {launchSpeed}";
        LaunchSpeedSlider.value = launchSpeed;
    }

    void OnMouseDrag() {
        Vector2 desiredMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (desiredMousePosition.y < startingPosition.y + 10 && desiredMousePosition.y > startingPosition.y - 30) {
            transform.position = new Vector2(transform.position.x, desiredMousePosition.y);
        }
        
    }

    void LaunchRocket() {
        Debug.Log("Launching");
        hasRocketLaunched = true;
        _rb.isKinematic = false;
        _rb.AddForce(Vector2.right * launchSpeed);
    }
}
