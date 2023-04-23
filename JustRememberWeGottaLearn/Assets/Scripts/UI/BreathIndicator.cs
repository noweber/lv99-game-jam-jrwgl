using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathIndicator : MonoBehaviour
{
    [SerializeField] private Vector3 m_startPosition;
    [SerializeField] private float m_segmentInterval = 0.2f;
    [SerializeField] private GameObject m_indicatorPrefab;
    [SerializeField] private Vector3 m_indicatorDownOffset = new Vector3(0, 1.0f, 0);
    
    private LineRenderer m_lineRenderer;
    private GameObject m_indicator;
    private int m_numSegments;

    Transform playerTransform;
    private Vector3 m_playerLastPosition;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_indicator = Instantiate(m_indicatorPrefab);
        m_lineRenderer.startColor = Color.green;
        m_lineRenderer.endColor = Color.red;
        m_lineRenderer.startWidth = 0.3f;
        m_lineRenderer.endWidth = 0.3f;
        m_lineRenderer.sortingLayerName = "Front";
        m_lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

    }

    private void Start()
    {
        playerTransform = Player.Instance.transform;
        UpdateSegementCount();
        TempoSystem.Instance.OnBFLimitChange += UpdateSegementCount;
    }

    private void UpdateSegementCount()
    {
        m_numSegments = (TempoSystem.Instance.MaxBF - TempoSystem.Instance.MinBF) / 5;
        //Debug.Log(m_numSegments);
        m_lineRenderer.positionCount = m_numSegments;
        

    }

    private void LateUpdate()
    {
        if (playerTransform)
        {
            Vector3 targetPosition = transform.position + (playerTransform.position - m_playerLastPosition);
            m_playerLastPosition = playerTransform.position;
            transform.position = targetPosition;

        }

        int segmentIndex = (TempoSystem.Instance.BreathFrequency - TempoSystem.Instance.MinBF );
        m_indicator.transform.position = transform.position +  (m_startPosition + Vector3.right * (m_segmentInterval / 5) * segmentIndex) + m_indicatorDownOffset;

        for (int i = 0; i < m_numSegments; i++)
        {
            //Debug.Log(m_startPosition + Vector3.right * m_segmentInterval * i);
            m_lineRenderer.SetPosition(i, transform.position + m_startPosition + Vector3.right * m_segmentInterval * i);
        }

    }
}
