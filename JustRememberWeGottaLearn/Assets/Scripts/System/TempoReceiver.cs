using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TempoReceiver : Singleton<TempoReceiver>
{
   
    
    private List<KungFuBeat> m_beats = new List<KungFuBeat>();
    [SerializeField] private float detectionZoneRadius = 1.0f;
    public Action<Vector3, string> OnHitTextPopup;

    public float LeftDetectionPointX
    {
        get { return transform.position.x - detectionZoneRadius; }
    }

    public float RightDetectionPointX
    {
        get { return transform.position.x + detectionZoneRadius; }
    }
    public Action<bool> OnBeatMiss;

    private void Start()
    {
       TempoSystem.Instance.OnTryHit += TryHit;
       OnHitTextPopup += (Vector3 position, string text) => { TextPopup.Create(position, text); };
    }
    public void AddMe(KungFuBeat beat)
    {
        m_beats.Add(beat);
    }

    private void TryHit()
    {
        if(m_beats.Count == 0)
        {
            OnBeatMiss.Invoke(true);
        }
        else
        {
            OnHitTextPopup?.Invoke(transform.position, "Breath");
            OnBeatMiss.Invoke(false);
            KungFuBeat firstBeat = m_beats[0];
            m_beats.RemoveAt(0);
            firstBeat.Hide();
        }

    }

    private void FixedUpdate()
    {

        for(int i = 0; i < m_beats.Count; i++)
        {
            KungFuBeat beat = m_beats[i];
            if (beat.transform.position.x >= RightDetectionPointX)
            {
                beat.Hide();
                OnBeatMiss.Invoke(true);
                m_beats.RemoveAt(i);
                i--;
            }
        }
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + detectionZoneRadius * Vector3.left, 0.1f);
        Gizmos.DrawWireSphere(transform.position + detectionZoneRadius * Vector3.right, 0.1f);
        Gizmos.DrawLine(transform.position + detectionZoneRadius * Vector3.left, transform.position + detectionZoneRadius * Vector3.right);
     
    }

}
