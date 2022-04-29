using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rocketv3 : MonoBehaviour
{
  [SerializeField][Range(0, 2000)]
  private float _launchSpeed = 1000f;

  [SerializeField][Range(0, 2000)]
  private float _boosterSpeed = 1000f;

  [SerializeField]
  TextMeshProUGUI speedTMP;

  [SerializeField]
  TextMeshProUGUI accelerationTMP;

  private Rigidbody2D _rb;

  public float _currentGravitationalAcceleration = 0f;

  private bool _hasRocketLaunched;

  public bool _rocketInGravityWell;
 
  private float _speed;

  void Awake()
  {
      _rb = GetComponent<Rigidbody2D>();
  }

  void Start()
  {
    _rb.isKinematic = true;
    _hasRocketLaunched = false;
  }

  void Update()
  {
    if (!_hasRocketLaunched) 
    {
      if (Input.GetKey(KeyCode.Space)) 
      {
        LaunchRocket();
      }
    }

    _speed = _rb.velocity.magnitude;

    if (_rocketInGravityWell) 
    {
      if (Input.GetKeyDown(KeyCode.D)) 
      {
        ApplyBooster();
      }

      if (Input.GetKeyDown(KeyCode.A)) 
      {
        ApplyBrake();
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

  void FixedUpdate()
  {
    speedTMP.text = $"Speed: {_speed}";
    accelerationTMP.text = $"Gravitational Acceleration: {_currentGravitationalAcceleration}";
  }

  private void LaunchRocket() 
  {
    Debug.Log("Launching");
    _hasRocketLaunched = true;
    _rb.isKinematic = false;
    _rb.AddForce(Vector2.right * _launchSpeed);
  }

  private void ApplyBooster() 
  {
    _rb.AddForce(Vector2.right * _boosterSpeed);
  }

  private void ApplyBrake() 
  {
    _rb.AddForce(Vector2.left * _boosterSpeed);
  }
}
