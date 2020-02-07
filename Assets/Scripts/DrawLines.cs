using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour //ладно, тут я понял, что точно не справился с заданием
{
    private List<Transform> linePoints = new List<Transform>();
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    private void Update()
    {
        AddPoint();
    }
    void FixedUpdate()
    {
        Debug.Log(lineRenderers.Count);
        UpdatePoints();
    }

    public void RemovePoint(Transform point)
    {
        if(linePoints.Contains(point) && linePoints.Count > 1 && linePoints.Count % 2 == 0)
        {
            RemoveTwoPoints(point);
        }
        else if(linePoints.Contains(point) && linePoints.Count > 1 && linePoints.Count % 2 == 1)
        {
            if(linePoints.IndexOf(point) == linePoints.Count - 1)
            {
                linePoints.RemoveAt(linePoints.Count - 1);
                lineRenderers.RemoveAt(linePoints.Count - 1);
            }
            else
            {
                RemoveTwoPoints(point);
            }
        }
        else if(linePoints.Contains(point) && linePoints.Count == 1)
        {
            linePoints.RemoveAt(0);
            lineRenderers.RemoveAt(0);
        }
    }

    private void RemoveTwoPoints(Transform point)
    {
        int indexOfPoint = linePoints.IndexOf(point);
        if (indexOfPoint % 2 == 0)
        {
            linePoints.RemoveAt(indexOfPoint);
            lineRenderers.RemoveAt(indexOfPoint);
            linePoints.RemoveAt(indexOfPoint + 1);
            lineRenderers.RemoveAt(indexOfPoint + 1);
        }
        else
        {
            linePoints.RemoveAt(indexOfPoint);
            lineRenderers.RemoveAt(indexOfPoint);
            linePoints.RemoveAt(indexOfPoint - 1);
            lineRenderers.RemoveAt(indexOfPoint - 1);
        }
    }
    private void AddPoint()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if(hit && Input.GetMouseButtonDown(1))
        {
            LineRenderer lineRenderer = hit.collider.gameObject.GetComponent<LineRenderer>();
            if(!lineRenderer)
            {
                lineRenderer = hit.collider.gameObject.AddComponent<LineRenderer>();
                lineRenderer.SetWidth(0.3f, 0.3f);
                lineRenderers.Add(lineRenderer);
            }

            if (!linePoints.Contains(hit.transform))
            {
                linePoints.Add(hit.transform);
            }
        }
    }

    private void UpdatePoints()
    {
        if(linePoints.Count > 1 && linePoints.Count % 2 == 0)
        {
            for(int i = 0; i <= linePoints.Count - 2; i += 2)
            {
                lineRenderers[i].SetPosition(0, linePoints[i].position);
                lineRenderers[i].SetPosition(1, linePoints[i+1].position);
            }
        }
    }
}
