using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRect : MonoBehaviour
{
    public float width = 1f;
    public float height = 1f;
    public Color lineColor = Color.white;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 5;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material.color = lineColor;

        Draw();
    }

    private void Draw()
    {
        Vector3[] rectangleCorners = new Vector3[5]
        {
            new Vector3(-width / 2f, -height / 2f, 0f),
            new Vector3(width / 2f, -height / 2f, 0f),
            new Vector3(width / 2f, height / 2f, 0f),
            new Vector3(-width / 2f, height / 2f, 0f),
            new Vector3(-width / 2f, -height / 2f, 0f)
        };

        for (int i = 0; i < rectangleCorners.Length; i++)
        {
            lineRenderer.SetPosition(i, rectangleCorners[i]);
        }
    }
}