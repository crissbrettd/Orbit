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

    private Vector2 gravityWellSize;

    private Rocket orbitingRocket;

    // Start is called before the first frame update
    void Start()
    {
        SetGravityWell();
    }

    // Update is called once per frame
    void Update()
    {
        gravityWellSize = GravityWell.transform.localScale;
        gravityWellSize.x = RelativeGravityWellRadius * 2;
        gravityWellSize.y = RelativeGravityWellRadius * 2;
        GravityWell.transform.localScale = gravityWellSize;
    }
    void FixedUpdate()
    {
        //Debug.Log
    }

    void SetGravityWell()
    {
        SpriteRenderer gws = GravityWell.GetComponent<SpriteRenderer>();
        Color gravityWellColor = gws.color;
        gravityWellColor.a = GravityFieldOpacity / 100f;
        gws.color = gravityWellColor;
    }
}
