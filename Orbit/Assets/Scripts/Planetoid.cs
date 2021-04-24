using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetoid : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)]
    public float _relativeGravityWellRadius = 1.5f;

    [SerializeField]
    public GameObject _gravityWell;

    [SerializeField]
    [Range(0, 100)]
    public float _gravityFieldOpacity = 50f;

    [SerializeField]
    [Range(0, 5)]
    public float _planetGravity = 1f;

    [SerializeField]
    public Rocket rocket;

    public bool rocketInGravityWell = false;

    private float currentGravityWellRadius;
    private Vector2 gravityWellSize;
    private Vector2 rocketToPlanetDirection;
    private float rocketToPlanetDistance;
    private Rigidbody2D rocketRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        SetGravityWell();
        currentGravityWellRadius = _relativeGravityWellRadius;
        rocketRigidbody = rocket.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rocketToPlanetDirection = (transform.position - rocket.transform.position).normalized;
        rocketToPlanetDistance = (transform.position - rocket.transform.position).magnitude;

        if (rocketInGravityWell)
        {
            float gravPercent = rocketToPlanetDistance / currentGravityWellRadius / 10;
            Debug.Log(gravPercent);
            //if (usablePercentage > 0f)
            //{
            //    rocketRigidbody.AddForce(rocketToPlanetDirection * (_PlanetGravity * usablePercentage));
            //}
            rocketRigidbody.AddForce(rocketToPlanetDirection * _planetGravity);
            //rocketRigidbody.AddForce(rocketToPlanetDirection * _planetGravity * gravPercent);
        }
    }
    void FixedUpdate()
    {
        if (currentGravityWellRadius != _relativeGravityWellRadius)
        {
            SetGravityWell();
        }
    }

    void SetGravityWell()
    {
        SpriteRenderer gws = _gravityWell.GetComponent<SpriteRenderer>();
        Color gravityWellColor = gws.color;
        gravityWellColor.a = _gravityFieldOpacity / 100f;
        gws.color = gravityWellColor;

        gravityWellSize = _gravityWell.transform.localScale;
        gravityWellSize.x = _relativeGravityWellRadius * 2;
        gravityWellSize.y = _relativeGravityWellRadius * 2;
        _gravityWell.transform.localScale = gravityWellSize;
    }
}
