using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class TempoGenerator : Singleton<TempoGenerator>
{
    [SerializeField] private KungFuBeat kungFuBeat;

    private List<KungFuBeat> m_beatsPool = new List<KungFuBeat>();
    private float m_timeUtilNextBeat;
    public const int NumBeatsInPool = 30;
    private int m_nextBeatToSpawn;
    private BPM m_bpm;

    public override void Awake()
    {
        base.Awake();
        
        //Populate the beats pool
        for(int i = 0; i < NumBeatsInPool; i++)
        {

            KungFuBeat instance = Instantiate(kungFuBeat, transform.position, Quaternion.identity);
            m_beatsPool.Add(instance);
            instance.SetSpawnPosition(transform);

            instance.transform.parent = transform.parent;
            instance.gameObject.SetActive(false);
           
        }
        m_nextBeatToSpawn = 0;
      
    }
    private void Start()
    {
        m_bpm = TempoSystem.Instance.StartBpm;
        TempoSystem.Instance.OnBpmChange += UpdateMyBpm;
    }

    private void UpdateMyBpm(BPM bpm)
    {
        m_bpm = bpm;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_timeUtilNextBeat > 0)
        {
            m_timeUtilNextBeat -= Time.deltaTime;
            return;
        }

        SpawnBeat();
        m_timeUtilNextBeat = 60.0f / (int)m_bpm;
    }
    private void SpawnBeat()
    {
        //Debug.Log("SpawnBeat");
        m_beatsPool[m_nextBeatToSpawn].gameObject.SetActive(true);
        m_nextBeatToSpawn++;
        m_nextBeatToSpawn = m_nextBeatToSpawn % (NumBeatsInPool);
        
    }
}
