using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    private GameObject _parentBody;
    private Planetoid planetoid => _parentBody.GetComponent<Planetoid>();

    // Start is called before the first frame update
    void Start()
    {
        _parentBody = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} entering the gravity well.");
        if (collision.gameObject.GetComponent<MassiveObject>() != null)
        {
            _parentBody.GetComponent<MassiveBody>()._fallingObjects.Add(collision.gameObject); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} exiting the gravity well.");
        if (collision.gameObject.GetComponent<MassiveObject>() != null)
        {
            _parentBody.GetComponent<MassiveBody>()._fallingObjects.Remove(collision.gameObject);
        }
    }
}
