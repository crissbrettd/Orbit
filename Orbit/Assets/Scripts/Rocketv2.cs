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

    public float _currentGravitationalAcceleration = 0f;

    private Vector2 startingPosition;
    private Vector2 currentAttemptStartingPosition;

    private float launchYSize;

    private Rigidbody2D _rb;

    private GameManager gameManager;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.isKinematic = true;
        hasRocketLaunched = false;

        gameManager = GameObject.Find("GameManager")
            .GetComponent<GameManager>();

        DontDestroyOnLoad(gameManager);

        // Get info of primary massive body in scene for positioning of rocket
        GameObject primaryBody = GameObject.Find("PrimaryBody");
        GameObject gravityWell = GameObject.Find("GravityWell");

        // Amount of movement for rocket in Y is based on size of the gravity well.
        launchYSize = gravityWell.transform.lossyScale.y / 2;

        // Rocket initially starts at Y of massive body
        startingPosition = new Vector2(
                transform.position.x, 
                primaryBody.transform.position.y);

        // Start rocket in last launch location if one is available
        if (gameManager.lastLaunchPosition == Vector2.zero) {
            currentAttemptStartingPosition = startingPosition;
        }
        else {
            currentAttemptStartingPosition = gameManager
                .GetLastLaunchPosition();
        }
        
        transform.position = currentAttemptStartingPosition;
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

    void FixedUpdate() {
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

    void AddSpeedToLaunch() {
        launchSpeed += 2f;
        speedTMP.text = $"Initial Speed: {launchSpeed}";
        LaunchSpeedSlider.value = launchSpeed;
    }

    void OnMouseDrag() {
        if (!hasRocketLaunched) {
            Vector2 desiredMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Rocket can only be dragged within bounds
            if (desiredMousePosition.y < startingPosition.y + launchYSize && desiredMousePosition.y > startingPosition.y - launchYSize) {
                transform.position = new Vector2(transform.position.x, desiredMousePosition.y);
            }
        }
    }

    void LaunchRocket() {
        gameManager.SetLastLaunchPosition(transform.position);
        Debug.Log("Launching");
        hasRocketLaunched = true;
        _rb.isKinematic = false;
        _rb.AddForce(Vector2.right * launchSpeed);
    }
}
