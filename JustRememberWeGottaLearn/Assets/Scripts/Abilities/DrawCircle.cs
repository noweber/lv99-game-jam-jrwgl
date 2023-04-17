using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Abilities
{
    using UnityEngine;

    public class DrawCircle : MonoBehaviour
    {
        public float radius = 1f;
        public int numSegments = 64;
        public Color lineColor = Color.white;

        private LineRenderer lineRenderer;
        private float angle;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.positionCount = numSegments + 1;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material.color = lineColor;
        }

        private void Update()
        {
            angle = 0f;

            for (int i = 0; i < numSegments + 1; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
                angle += 360f / numSegments;
            }
        }
    }

}
