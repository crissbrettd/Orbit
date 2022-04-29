using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    [SerializeField]
    GameObject OrbitLinePrefab;

    private GameObject _parentBody;
    private Vector2 OrbitEntranceCoordinates;

    [SerializeField]
    private float lineWidth = 0.1f;

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

        OrbitEntranceCoordinates = collision.gameObject.transform.position;

        if (collision.gameObject.GetComponent<MassiveObject>() != null)
        {
            _parentBody.GetComponent<MassiveBody>()._fallingObjects.Add(collision.gameObject); 
            collision.gameObject.GetComponent<Rocketv3>()._rocketInGravityWell = true;
        }

        CreateOrbitLine(OrbitEntranceCoordinates);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} exiting the gravity well.");
        if (collision.gameObject.GetComponent<MassiveObject>() != null)
        {
            _parentBody.GetComponent<MassiveBody>()._fallingObjects.Remove(collision.gameObject);
            collision.gameObject.GetComponent<Rocketv3>()._rocketInGravityWell = false;
        }
    }

    void CreateOrbitLine(Vector2 coords) {
        Vector2 start = transform.position;
        Vector2 end = coords;
        Vector3[] positions = { start, end };
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;

        GameObject emptyColliderObject = new GameObject();
        emptyColliderObject.AddComponent<OrbitLapLine>();
        emptyColliderObject.transform.SetParent(lineRenderer.transform);
        BoxCollider2D b = emptyColliderObject.AddComponent<BoxCollider2D>();
        b.isTrigger = true;

        float length = Vector2.Distance(start, end);
        b.size = new Vector2(length, lineWidth);
        Vector2 colliderPos = ((Vector2)start + (Vector2)end) / 2;
        b.transform.position = colliderPos;

        float angle = (Mathf.Abs (start.y - end.y) / Mathf.Abs (start.x - end.x));
        if((start.y < end.y && start.x > end.x) || (end.y < start.y && end.x > start.x))
        {
            angle*=-1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan (angle);
        b.transform.Rotate (0, 0, angle);
    }
}
