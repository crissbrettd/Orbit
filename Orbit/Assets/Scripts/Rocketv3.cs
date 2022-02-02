using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketv3 : MonoBehaviour
{
  [SerializeField][Range(0, 2000)]
  private float _launchSpeed = 1000f;

  private Rigidbody2D _rb;

  private bool _hasRocketLaunched;

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
      // if (Input.GetKey(KeyCode.Space)) {
      //     AddSpeedToLaunch();            
      // }

      if (Input.GetKey(KeyCode.Space)) 
      {
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

  private void LaunchRocket() 
  {
    Debug.Log("Launching");
    _hasRocketLaunched = true;
    _rb.isKinematic = false;
    _rb.AddForce(Vector2.right * _launchSpeed);
  }
}
