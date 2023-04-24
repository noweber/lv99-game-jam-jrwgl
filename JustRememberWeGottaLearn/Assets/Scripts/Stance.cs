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

    [SerializeField] private stance startStance = stance.BruceLee;
    public enum stance
    {
        ShaolinKungFu,
        WingChun,
        BruceLee,
        OutOfBreath
    }

    public override void Awake()
    {
        base.Awake();
        currentStance = startStance;
    }
    private void Start()
    {
        
        TempoSystem.Instance.OnBpmChange += DoStanceUpdate;
    }
   
    private void DoStanceUpdate(BPM bpm)
    {
        stance oldStance = currentStance;
        switch (bpm)
        {
            case BPM.bpm30:
                currentStance = stance.BruceLee;
                break;
            case BPM.bpm60:
                currentStance = stance.ShaolinKungFu;
                break;
            case BPM.bpm90:
                currentStance = stance.WingChun;
                break;
            case BPM.bpm120:
                currentStance = stance.OutOfBreath;
                break;
        }

        if(oldStance != currentStance)
        {
            onSwitchStance.Invoke();
        }
    }

}


