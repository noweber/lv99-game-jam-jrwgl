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

public class TempoGenerator : Singleton<TempoGenerator>
{
    // Start is called before the first frame update
    
    [SerializeField] private BPM bpm;
    [SerializeField] private KungFuBeat kungFuBeat;
    [SerializeField] private int BreathFrequency = 30;

    [SerializeField] private int attackBFIncrease = 3;
    [SerializeField] private int dashBFIncrease = 2;
    [SerializeField] private int bfNaturalDecreaseAmount = 1;
    [SerializeField] private float bfNaturalDecreaseTimeInterval = 0.2f;
    [SerializeField] private float TakeDeepBreathInterval = 0.05f;

    private bool isHarmony;


    private float _timeUtilNextBeat;
    private float _timeUtilNextBFDecrease;
    private float _timeUtilNextDeepBreath;
   


    private List<KungFuBeat> beatsPool = new List<KungFuBeat>();
    public const int numBeatsInPool = 30;

    private int nextBeatToSpawn;
    private int headBeat;
    private bool isTakingDeepBreath;
 
    public Action<BPM> OnBpmChange;

    public override void Awake()
    {
        base.Awake();
        
        //Populate the beats pool
        for(int i = 0; i < numBeatsInPool; i++)
        {

            KungFuBeat instance = Instantiate(kungFuBeat, transform.position, Quaternion.identity);
            beatsPool.Add(instance);
            instance.SetSpawnPosition(transform);
            instance.OnBeatHit += SetToHarmony;
            instance.OnBeatMiss += SetToNotHarmony;
            
            instance.gameObject.SetActive(false);
            instance.Init();
        }
        nextBeatToSpawn = 0;
        headBeat = 0;

    }


    private void Start()
    {

        //TryStartGenerate(Stance.stance.BruceLee, 100, 60);
        TempoReceiver.Instance.OnBeatReceived += DestroyHeadbeat;
        //TryStartGenerate();
        Player.Instance.OnPlayerAttack += DoAttackBFIncrease;
        Player.Instance.OnPlayerDash += DoDashBFIncrease;
        Player.Instance.OnTakeDeepBreath += HandleTakeBreath;
        Player.Instance.OnStopTakeBreath += HandleStopTakingBreath;
        

        bpm = BPM.bpm30;
    }
    private void HandleTakeBreath()
    {
        isTakingDeepBreath = true;
    }
    private void HandleStopTakingBreath()
    {
        isTakingDeepBreath = false;
    }

    public int GetBreathFrequency()
    {
        return BreathFrequency;
    }
    private void DoAttackBFIncrease()
    {
        if (!isHarmony)
        {
            BreathFrequency += attackBFIncrease;
            TryUpdateBPM();
        }
    }

    private void DoDashBFIncrease()
    {
        if (!isHarmony)
        {
            BreathFrequency += dashBFIncrease;
            TryUpdateBPM();
        }
    }

    private void SetToHarmony()
    {
        isHarmony = true;
    }
    private void SetToNotHarmony()
    {
        isHarmony = false;
    }

    private void BFNaturalDecrease()
    {
        _timeUtilNextBFDecrease -= Time.deltaTime;
        if (!isHarmony)
        {
            if (_timeUtilNextBFDecrease <= 0)
            {
                //Debug.Log("Natural decrese bpm");
                _timeUtilNextBFDecrease = bfNaturalDecreaseTimeInterval;
                BreathFrequency -= bfNaturalDecreaseAmount;
                if(BreathFrequency < 30)
                {
                    BreathFrequency = 30;
                }
                TryUpdateBPM();
            }
        }

    }

    private void TakeDeepBreath()
    {
        _timeUtilNextDeepBreath -= Time.deltaTime;
        isHarmony = true;

        if(_timeUtilNextDeepBreath <= 0)
        {
            if(bpm == BPM.bpm180plus)
                _timeUtilNextDeepBreath = TakeDeepBreathInterval * 10;
            else
                _timeUtilNextDeepBreath = TakeDeepBreathInterval;
            
            BreathFrequency -= 1;
            if(BreathFrequency <30)
            {
                BreathFrequency = 30;
            }
            TryUpdateBPM();
        }
    }


    private void TryUpdateBPM()
    {
        BPM oldBpm = bpm;
        if (BreathFrequency < 45)
            bpm = BPM.bpm30;
        else if (BreathFrequency <= 75)
            bpm = BPM.bpm60;
        else if (BreathFrequency <= 105)
            bpm = BPM.bpm90;
        else if (BreathFrequency <= 135)
            bpm = BPM.bpm120;
        else if (BreathFrequency <= 165)
            bpm = BPM.bpm150;
        else if (BreathFrequency <= 195)
            bpm = BPM.bpm180;
        else
            bpm = BPM.bpm180plus;


        if(bpm != oldBpm)
        {
            //bpm has changed.
            OnBpmChange.Invoke(bpm);
            Debug.Log("Update BPM to " + bpm.ToString());
        }
        if(bpm == BPM.bpm180plus && !isTakingDeepBreath)
        {
            //Try to discourage player to go over the breath limit.
            BreathFrequency += 20;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isTakingDeepBreath)
        {
            TakeDeepBreath();
        }
        else if(bpm != BPM.bpm180plus)
        {
            BFNaturalDecrease();
        }

        if (_timeUtilNextBeat > 0)
        {
            _timeUtilNextBeat -= Time.deltaTime;
            return;
        }

        //Instantiate(kungFuBeat, transform.position, Quaternion.identity);
        SpawnBeat();

        //Debug.Log(bpm);
        //Debug.Log((int)bpm);
        
        _timeUtilNextBeat = 60.0f / (int)bpm;

      

    }

    
    private void SpawnBeat()
    {
        //Debug.Log("SpawnBeat");
        beatsPool[nextBeatToSpawn].gameObject.SetActive(true);
       
        nextBeatToSpawn++;
        nextBeatToSpawn = nextBeatToSpawn % (numBeatsInPool);
        
    }

    private void DestroyHeadbeat()
    {
        //Debug.Log("Destroy Head beat");
        if (beatsPool[headBeat].gameObject.activeInHierarchy)
        {
            beatsPool[headBeat].gameObject.SetActive(false);
            headBeat += 1;
            headBeat = headBeat % (numBeatsInPool);
            

        }
           
    }

    public void DoCleanUp()
    {
        
        foreach(KungFuBeat kfbeat in beatsPool)
        {
            kfbeat.DoBeforeDestroy();
        }
        
        DG.Tweening.DOTween.KillAll();
    }



}
