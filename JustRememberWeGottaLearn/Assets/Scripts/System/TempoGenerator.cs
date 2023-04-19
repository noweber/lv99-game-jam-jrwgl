using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TempoGenerator : Singleton<TempoGenerator>
{
    // Start is called before the first frame update

    [SerializeField] private int BPM;
    [SerializeField] private int numOfBeatsToGenerate;
    [SerializeField] private KungFuBeat kungFuBeat;

    private float _timeUtilNextBeat;
    private int remainBeatCount;
    private bool isGenerating;
    private float beatsGenerateInterval;


    private List<KungFuBeat> beatsPool = new List<KungFuBeat>();
    public const int numBeatsInPool = 30;

    private int nextBeatToSpawn;
    private int headBeat;
    private Stance.stance generateStanceBeats;

    public Action<Stance.stance> OnHeadBeatDestroy;
    

    public override void Awake()
    {
        base.Awake();
        isGenerating = false;
        //Populate the beats pool
        for(int i = 0; i < numBeatsInPool; i++)
        {

            KungFuBeat instance = Instantiate(kungFuBeat, transform.position, Quaternion.identity);
            beatsPool.Add(instance);
            instance.GetComponent<KungFuBeat>().SetSpawnPosition(transform);
            instance.gameObject.SetActive(false);
            instance.GetComponent<KungFuBeat>().Init();
            
        }

        nextBeatToSpawn = 0;
        headBeat = 0;

    }

    private void Start()
    {
        
        TempoReceiver.Instance.OnBeatReceived += DestroyHeadbeat;
        //TryStartGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        if(remainBeatCount == 0)
        {
            return;
        }
        
        if(_timeUtilNextBeat > 0)
        {
            _timeUtilNextBeat -= Time.deltaTime;
            return;
        }

        //Instantiate(kungFuBeat, transform.position, Quaternion.identity);
        SpawnBeat();
        remainBeatCount -= 1;
        _timeUtilNextBeat = beatsGenerateInterval;

        if(remainBeatCount == 0)
        {
            isGenerating = false;
        }

    }

    private void SpawnBeat()
    {
        beatsPool[nextBeatToSpawn].gameObject.SetActive(true);
        beatsPool[nextBeatToSpawn]._stance = generateStanceBeats;
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
            if (beatsPool[headBeat].gameObject.activeInHierarchy)
            {
                OnHeadBeatDestroy.Invoke(beatsPool[headBeat]._stance);
            }
            else
            {
                OnHeadBeatDestroy.Invoke(Stance.stance.Balance);
            }

        }
           
    }

    
    public bool TryStartGenerate(Stance.stance generateStance, int numToGenerate, int _BPM)
    {
        if(isGenerating)
        {
           // Debug.LogWarning("Already generating");
            return false;
        }
        Debug.Log("Generating at ");
        Debug.Log(generateStance);

        generateStanceBeats = generateStance;
        BPM = _BPM;

        isGenerating = true;
        beatsGenerateInterval = 60.0f / BPM;
        remainBeatCount = numToGenerate;
        _timeUtilNextBeat = beatsGenerateInterval;
        return true;
    }
}
