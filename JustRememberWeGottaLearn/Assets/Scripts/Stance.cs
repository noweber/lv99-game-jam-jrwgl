using QFSW.QC.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stance : Singleton<Stance>
{
    [SerializeField] private int BeatPerGeneration;
    [SerializeField] private int TunaBPM;
    [SerializeField] private int ShaolinBPM;
    [SerializeField] private int BruceLeeBPM;
    [SerializeField] private int WingChunBPM;


    public stance currentStance;
    public Action onSwitchStance;

    private List<stance> stanceList = new List<stance>();

    

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
        
    }
    private void Start()
    {
        TempoGenerator.Instance.OnHeadBeatDestroy += DoHeadBeatDestroy;
    }

    private void Update()
    {

        //onSwitchStance.Invoke();
        //currentStance = stance.ShaolinKungFu;

        //Add stance
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stanceList.Add(stance.TunaTechnique);
                
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            stanceList.Add(stance.ShaolinKungFu);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            stanceList.Add(stance.BruceLee);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            stanceList.Add(stance.WingChun);
        }


        if (stanceList.Count > 0)
        {
            if (stanceList[0] == stance.ShaolinKungFu)
            {
                if (TempoGenerator.Instance.TryStartGenerate(stanceList[0], BeatPerGeneration, ShaolinBPM))
                {
                    stanceList.RemoveAt(0);
                }
            }
            else if (stanceList[0] == stance.WingChun)
            {
                if (TempoGenerator.Instance.TryStartGenerate(stanceList[0], BeatPerGeneration, WingChunBPM))
                {
                    stanceList.RemoveAt(0);
                }
            }
            else if (stanceList[0] == stance.BruceLee)
            {
                if (TempoGenerator.Instance.TryStartGenerate(stanceList[0], BeatPerGeneration, BruceLeeBPM))
                {
                    stanceList.RemoveAt(0);
                }
            }
            else if (stanceList[0] == stance.TunaTechnique)
            {
                if (TempoGenerator.Instance.TryStartGenerate(stanceList[0], BeatPerGeneration, TunaBPM))
                {
                    stanceList.RemoveAt(0);
                }
            }


        }




    }

    private void DoHeadBeatDestroy(stance _stance)
    {
        currentStance = _stance;
        onSwitchStance.Invoke();

    }


    
   

}


