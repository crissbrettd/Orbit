using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetoid : MonoBehaviour
{
    [SerializeField][Range(0, 3)]
    public float RelativeGravityWellRadius = 1.5f;

    [SerializeField]
    public GameObject GravityWell;

    [SerializeField][Range(0, 100)]
    public float GravityFieldOpacity = 50f;

    [SerializeField][Range(0, 5)] 
    public float PlanetGravity = 1f;

    [SerializeField]
    public Rocket rocket;

    public bool rocketInGravityWell = false;

    private Vector2 gravityWellSize;
    private Vector2 rocketToPlanetDirection;
    private Rigidbody2D rocketRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        SetGravityWell();
        rocketRigidbody = rocket.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rocketToPlanetDirection = (transform.position - rocket.transform.position).normalized;

        if (rocketInGravityWell)
        {
            rocketRigidbody.AddForce(rocketToPlanetDirection * PlanetGravity);
        }
    }
    void FixedUpdate()
    {
        
    }

    void SetGravityWell()
    {
        SpriteRenderer gws = GravityWell.GetComponent<SpriteRenderer>();
        Color gravityWellColor = gws.color;
        gravityWellColor.a = GravityFieldOpacity / 100f;
        gws.color = gravityWellColor;

        gravityWellSize = GravityWell.transform.localScale;
        gravityWellSize.x = RelativeGravityWellRadius * 2;
        gravityWellSize.y = RelativeGravityWellRadius * 2;
        GravityWell.transform.localScale = gravityWellSize;
    }
}
