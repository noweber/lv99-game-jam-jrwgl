using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnPD
{
    public int AlphaPD;
    public int BetaPD;
    public int OmegaPD;
    
    public int Total { get { return AlphaPD + BetaPD + OmegaPD; } }
}
public class WaveSpawner : Singleton<WaveSpawner>
{
    // Start is called before the first frame update
    public float NormalInterval = 15.0f;
    public float WaveInterval = 5.0f;
    public float WaveSpawnMultiplier = 4.0f;

    public float SpawnInterval = 8.0f;

    public GameObject Alpha;
    public GameObject Beta;
    public GameObject Omega;

    public List<Transform> SpawnLocation = new List<Transform>();
    public List<SpawnPD> SpawnWave = new List<SpawnPD>();

    public Action Win;


    private float m_remainWaveTime;
    private float m_remainSpawnTime;
    private int m_currentWave;

    private void Start()
    {
        m_remainWaveTime = m_remainWaveTime = NormalInterval + WaveInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_remainWaveTime <= 0)
        {
            if(m_currentWave == SpawnWave.Count - 1)
            {
                Win?.Invoke();
                Debug.Log("Win!!");
                Destroy(gameObject);
            }
            else
            {
                m_remainWaveTime = NormalInterval + WaveInterval;
                m_currentWave++;
                Debug.Log("Start Wave " + m_currentWave.ToString());
            }
        }
        {
            m_remainWaveTime -= Time.deltaTime;
        }

        if(m_remainSpawnTime <= 0)
        {
            Transform spawnPos = SpawnLocation[UnityEngine.Random.Range(0, SpawnLocation.Count)];
            int spawnType = UnityEngine.Random.Range(0, SpawnWave[m_currentWave].Total);

            if(spawnType < SpawnWave[m_currentWave].AlphaPD)
            {
                //Instantiate(Alpha, spawnPos.position, Quaternion.identity);
                SpawnAlpha(spawnPos.position);
            }
            else if(spawnType < SpawnWave[m_currentWave].AlphaPD + SpawnWave[m_currentWave].BetaPD)
            {
                SpawnBeta(spawnPos.position);
            }
            else
            {
                Instantiate(Omega, spawnPos.position, Quaternion.identity);

            }

            if (m_remainWaveTime <= WaveInterval) {
                Debug.Log("Enter Wave");
                m_remainSpawnTime = SpawnInterval / WaveSpawnMultiplier;
            }
            else
            {
                m_remainSpawnTime = SpawnInterval;
            }

        }
        else
        {
            m_remainSpawnTime -= Time.deltaTime;
        }

    }
    
    private void SpawnAlpha(Vector3 spawnPos)
    {
        if(m_currentWave <= 3)
        {
            Instantiate(Alpha, spawnPos, Quaternion.identity);
        }
        else if (m_currentWave <= 6)
        {
            Instantiate(Alpha, spawnPos, Quaternion.identity);
            Instantiate(Alpha, spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(Alpha, spawnPos, Quaternion.identity);
            Instantiate(Alpha, spawnPos, Quaternion.identity);
            Instantiate(Alpha, spawnPos, Quaternion.identity);
            Instantiate(Alpha, spawnPos, Quaternion.identity);
        }
    }

    private void SpawnBeta(Vector3 spawnPos)
    {
        if (m_currentWave <= 3)
        {
            for(int i = 0; i < 5; i ++)
            {
                Instantiate(Beta, spawnPos, Quaternion.identity);
            }
        }
        else if (m_currentWave <= 6)
        {
            for (int i = 0; i < 7; i++)
            {
                Instantiate(Beta, spawnPos, Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                Instantiate(Beta, spawnPos, Quaternion.identity);
            }
        }
    }



}
