using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MassiveBody : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    public float _massOfBody = 450f;

    [SerializeField]
    [Range(0, 3)]
    public float _relativeGravityWellRadius = 2f;

    [SerializeField]
    public GameObject _gravityWell;

    [SerializeField]
    [Range(0, 100)]
    public float _gravityFieldOpacity = 15f;

    [Space]

    [SerializeField]
    public List<GameObject> _fallingObjects;

    private float _radiusOfBody;
    private float _currentGravityWellRadius;
    private Vector2 gravityWellSize;

    // Start is called before the first frame update
    void Start()
    {
        _radiusOfBody = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fallingObjects.Count != 0)
        {
            foreach (GameObject o in _fallingObjects)
            {
                Vector2 objectToPlanetDirection = (transform.position - o.transform.position).normalized;
                float accelerationForce = ComputeGravitationalForceForObject(o);
                o.GetComponent<Rigidbody2D>().AddForce(accelerationForce * objectToPlanetDirection);

                if (o.GetComponent<Rocket>() != null)
                {
                    o.GetComponent<Rocket>()._currentGravitationalAcceleration = accelerationForce;
                }
             }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.LogWarning("Player impacted body.");
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        if (_currentGravityWellRadius != _relativeGravityWellRadius)
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

    float ComputeGravitationalForceForObject(GameObject mo)
    {
        float distance = Vector2.Distance(mo.transform.position, gameObject.transform.position);
        float height = distance - _radiusOfBody;

        // g=GM/(r+h)^2
        // G is gravitational constant and may not need to be applied?
        float gravitationalAccelerationForce = _massOfBody / ((_radiusOfBody + height) * (_radiusOfBody + height));

        return gravitationalAccelerationForce;
    }
}
