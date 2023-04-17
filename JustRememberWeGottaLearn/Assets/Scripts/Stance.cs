using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stance : Singleton<Stance>
{
    [SerializeField] private Vector2 BalanceFreqRange;
    [SerializeField] private Vector2 ShaolinKungFuFreqRange;
    [SerializeField] private Vector2 WingChunFreqRange;
    [SerializeField] private Vector2 BruceLeeFreqRange;

    [SerializeField] private float maxTimeFrameForOOB;


    private float _lastInFreqRangeTimeStamp;
    
    public stance currentStance;
    public Action onSwitchStance;
    public enum stance
    {
        Balance,
        OutOfBreath,
        ShaolinKungFu,
        WingChun,
        BruceLee
    }

    private void Awake()
    {
        currentStance = stance.Balance;
        _lastInFreqRangeTimeStamp = Time.time;
    }

    private void Update()
    {
        /*
        if(Time.time - _lastInFreqRangeTimeStamp > maxTimeFrameForOOB)
        {
            //Switch stance because out of breath;
            currentStance = stance.OutOfBreath;
            onSwitchStance.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            //Switch Stance Actively
            currentStance = FreqInWhatStance(RhythmSystem.Instance.GetFrequency());
            onSwitchStance.Invoke();
        }

        if(FreqInWhatStance(RhythmSystem.Instance.GetFrequency()) == currentStance)
        {
            _lastInFreqRangeTimeStamp = Time.time;
        }
        */

        if (FreqInWhatStance(RhythmSystem.Instance.GetFrequency()) != currentStance)
        {
            currentStance = FreqInWhatStance(RhythmSystem.Instance.GetFrequency());
            onSwitchStance.Invoke();
        }



    }

    private stance FreqInWhatStance(float freq)
    {
        if(freq < BalanceFreqRange[1] && freq > BalanceFreqRange[0]){
            return stance.Balance;
        }
        else if (freq < ShaolinKungFuFreqRange[1] && freq > ShaolinKungFuFreqRange[0])
        {
            return stance.ShaolinKungFu;
        }
        else if (freq < WingChunFreqRange[1] && freq > WingChunFreqRange[0])
        {
            return stance.WingChun;
        }

        else if (freq < BruceLeeFreqRange[1] && freq > BruceLeeFreqRange[0])
        {
            return stance.BruceLee;
        }



        return stance.Balance;
    }

    
}


