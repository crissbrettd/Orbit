using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    private GameObject parentPlanet;
    private Planetoid planetoid => parentPlanet.GetComponent<Planetoid>();

    // Start is called before the first frame update
    void Start()
    {
        parentPlanet = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} entering the gravity well.");
        if (collision.gameObject.tag == "Rocket")
        {
            planetoid.rocketInGravityWell = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} exiting the gravity well.");
        if (collision.gameObject.tag == "Rocket")
        {
            planetoid.rocketInGravityWell = false;
        }
    }
}
