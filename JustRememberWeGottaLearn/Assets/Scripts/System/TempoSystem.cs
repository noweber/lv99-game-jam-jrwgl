using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BPM
{
    bpm30 = 30,
    bpm60 = 60,
    bpm90 = 90,
    bpm120 = 120,
    bpm150 = 150,
    bpm180 = 180,
    bpm180plus = 300

}

public enum BreathState
{
    Normal,
    Harmony,
    TakingDeepBreath
}

public class TempoSystem : Singleton<TempoSystem>
{
    // Start is called before the first frame update

    public BPM StartBpm = BPM.bpm30;
    [SerializeField] private int m_minBF = 60;
    [SerializeField] private int m_maxBF = 120;
    [SerializeField] private float m_timeNeededToEnterBreathingState = 1.6f;
    //[SerializeField] private BPM StartBpm = BPM.bpm30;
    
    [SerializeField] private float TakeDeepBreathInterval = 0.05f;
    [SerializeField] private int HitCountToHarmony = 1;

    private BPM m_bpm;
    private int m_breathFrequency;
    private float m_timeUtilNextDeepBreath;
    private float m_timeRemainToEnterBreathingState;
    private BreathState m_state;
    private int m_hitCount;

    public Action<BPM> OnBpmChange;
    public Action OnTryHit;
    public Action OnBFLimitChange;

    public int BreathFrequency { get { return m_breathFrequency; } }
    public int MinBF { get { return m_minBF; } }
    public int MaxBF { get { return m_maxBF; } }    

    public override void Awake()
    {
        base.Awake();
        m_bpm = StartBpm;
        m_breathFrequency = m_minBF;
    }
    public void DecrementHarmonyHitCount()
    {
        HitCountToHarmony--;
    }
    private void Start()
    {

        //Player.Instance.OnTakeDeepBreath += HandleTakeBreath;
        //Player.Instance.OnStopTakeBreath += HandleStopTakingBreath;

        foreach (KungFu kungfu in Player.Instance.GetComponents<KungFu>())
        {
            kungfu.OnKungFuAttack += HandleAttack;
            kungfu.OnKungFuDash += HandleDash;
        }
        TempoReceiver.Instance.OnBeatMiss += HandleBeatMiss;
   
    }
    public void DecrementBFLimt(int amount)
    {
        m_minBF -= amount;
        OnBFLimitChange?.Invoke();
    }
    public void IncrementBFLimit(int amount)
    {
        m_maxBF += amount;
        OnBFLimitChange?.Invoke();
    }


    private void HandleBeatMiss(bool miss)
    {
        if (m_state != BreathState.TakingDeepBreath)
        {
            if (miss)
            {
                m_hitCount = 0;
                m_state = BreathState.Normal;
            }
            else
            {
                m_hitCount++;
                if (m_hitCount >= HitCountToHarmony)
                {
                    m_state = BreathState.Harmony;
                }
            }
        }
    }

    /*
    private void HandleTakeBreath()
    {
        m_state = BreathState.TakingDeepBreath;
    }
    private void HandleStopTakingBreath()
    {
        m_state = BreathState.Harmony;
    }
    */

    
    private void HandleAttack(int attackBFIncrease)
    {
        OnTryHit.Invoke();
        if (m_state != BreathState.Harmony)
        {
            m_breathFrequency += attackBFIncrease;
;           m_state = BreathState.Normal;
            m_timeRemainToEnterBreathingState = m_timeNeededToEnterBreathingState;
            TryUpdateBPM();
        }
    }
    private void HandleDash(int dashBFIncrease)
    {
        OnTryHit.Invoke();
        if (m_state != BreathState.Harmony)
        {
            m_breathFrequency += dashBFIncrease;
            m_state = BreathState.Normal;
            m_timeRemainToEnterBreathingState = m_timeNeededToEnterBreathingState;
            TryUpdateBPM();
        }
    }
    

   
    private void TakeDeepBreath()
    {
        m_timeUtilNextDeepBreath -= Time.deltaTime;

        if (m_timeUtilNextDeepBreath <= 0)
        {
            if (m_bpm == BPM.bpm180plus)
                m_timeUtilNextDeepBreath = TakeDeepBreathInterval * 10;
            else
                m_timeUtilNextDeepBreath = TakeDeepBreathInterval;

            m_breathFrequency -= 1;
            if (m_breathFrequency < m_minBF)
            {
                m_breathFrequency = m_minBF;
            }
            TryUpdateBPM();
        }
    }


    private void TryUpdateBPM()
    {
        //for breath bgm test
        //AudioManager.instance.StartPlayBPM(m_bpm, 0);
        BPM oldBpm = m_bpm;

        if (m_breathFrequency >= m_maxBF)
        {
            m_bpm = BPM.bpm180plus;
        }
        else
        {
            if (m_breathFrequency < 45)
                m_bpm = BPM.bpm30;
            else if (m_breathFrequency <= 75)
                m_bpm = BPM.bpm60;
            else if (m_breathFrequency <= 105)
                m_bpm = BPM.bpm90;
            else if (m_breathFrequency <= 135)
                m_bpm = BPM.bpm120;
            else if (m_breathFrequency <= 165)
                m_bpm = BPM.bpm150;
            else if (m_breathFrequency <= 195)
                m_bpm = BPM.bpm180;
            else
                m_bpm = BPM.bpm180plus;
        }

       

        if (m_bpm != oldBpm)
        {
            //bpm has changed.
            OnBpmChange.Invoke(m_bpm);
            Debug.Log("Update BPM to " + m_bpm.ToString());
        }
        if (m_bpm == BPM.bpm180plus && m_state != BreathState.TakingDeepBreath)
        {
            //Try to discourage player to go over the breath limit.
            m_breathFrequency += 20;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (m_state == BreathState.TakingDeepBreath)
        {
            TakeDeepBreath();
        }
        
        if(m_timeRemainToEnterBreathingState > 0)
        {
            m_timeRemainToEnterBreathingState -= Time.deltaTime;
        }
        else if(m_state == BreathState.Normal)
        {
            m_state = BreathState.TakingDeepBreath;
        }
    }

}
