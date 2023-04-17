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
    [SerializeField] private Vector2 TunaTechniqueFreqRange;

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
        BruceLee,
        TunaTechnique
    }

    private void Awake()
    {
        currentStance = stance.Balance;
        _lastInFreqRangeTimeStamp = Time.time;
    }

    private void Update()
    {
        
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

        else if (freq < TunaTechniqueFreqRange[1] && freq > TunaTechniqueFreqRange[0])
        {
            return stance.TunaTechnique;
        }



        return stance.Balance;
    }

    
}


