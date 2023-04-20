using QFSW.QC.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Stance : Singleton<Stance>
{
    public stance currentStance;
    public Action onSwitchStance;

    [SerializeField] private stance startStance = stance.ShaolinKungFu;
    public enum stance
    {
        Balance,
        OutOfBreath,
        ShaolinKungFu,
        WingChun,
        BruceLee,
        TunaTechnique
    }

    public override void Awake()
    {
        base.Awake();
        currentStance = startStance;
    }
    private void Start()
    {
        
        TempoGenerator.Instance.OnBpmChange += DoStanceUpdate;
    }

    private void Update()
    {


    }

    
    private void DoStanceUpdate(BPM bpm)
    {
        stance oldStance = currentStance;
        switch (bpm)
        {
            case BPM.bpm30:
                currentStance = stance.ShaolinKungFu;
                break;
            case BPM.bpm60:
                currentStance = stance.ShaolinKungFu;
                break;
            case BPM.bpm90:
                currentStance = stance.BruceLee;
                break;
            case BPM.bpm120:
                currentStance = stance.BruceLee;
                break;
            case BPM.bpm150:
                currentStance = stance.WingChun;
                break;
            case BPM.bpm180:
                currentStance = stance.WingChun;
                break;
            case BPM.bpm180plus:
                currentStance = stance.OutOfBreath;
                break;
        }

        if(oldStance != currentStance)
        {
            onSwitchStance.Invoke();
        }
    }

}


