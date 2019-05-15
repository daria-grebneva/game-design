using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EmptyObject : MonoBehaviour
{
    private int count;

    private LineRenderer lineRenderer;

    private Vector3 currPos;

    private Vector3 prevPos;

    private Collider other1;

    private bool firstCollision;
    
    private List<String> visitedDots = new List<String>();
    public List<String> GetVisitedDots()
    {
        return visitedDots;
    }

    // Start is called before the first frame update
    void Start()
    {
        visitedDots.Clear();
        firstCollision = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = GetCurrentMousePosition().GetValueOrDefault();
            transform.position = new Vector3(pos.x, pos.y, 90);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Dot>().isTrigger)
        {
            return;
        }
        if (firstCollision)
        {
            lineRenderer = other.GetComponent(typeof(LineRenderer)) as LineRenderer;
            prevPos = other.transform.position;
            other1 = other;
            firstCollision = false;
            visitedDots.Add(other.name);
            other.GetComponent<SpriteRenderer>().color = Color.green;
            return;
        }
        currPos = other.transform.position;
        lineRenderer.SetPosition(0, prevPos);
        lineRenderer.SetPosition(1, currPos);
        other1.GetComponent<Dot>().isLineOn = true;
        prevPos = currPos;
        lineRenderer = other.GetComponent(typeof(LineRenderer)) as LineRenderer;
        other1 = other;
        visitedDots.Add(other.name);
        other1.GetComponent<Dot>().isTrigger = false;
        other.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private Vector3? GetCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            Vector3 pos = ray.GetPoint(rayDistance);
            pos.z = 0;
            return pos;
        }

        return null;
    }
}