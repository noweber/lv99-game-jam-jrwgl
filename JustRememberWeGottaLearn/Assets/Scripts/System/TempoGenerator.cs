using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TempoGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int BPM;
    [SerializeField] private int numOfBeatsToGenerate;
    [SerializeField] private KungFuBeat kungFuBeat;

    private float _timeUtilNextBeat;
    private int remainBeatCount;
    private bool isGenerating;
    private float beatPerSecond;


    private List<KungFuBeat> beatsPool = new List<KungFuBeat>();
    private const int numBeatsInPool = 30;

    private int nextBeatToSpawn;
    private int headBeat;

    
    private void Awake()
    {
        isGenerating = false;
        //Populate the beats pool
        for(int i = 0; i < numBeatsInPool; i++)
        {

            KungFuBeat instance = Instantiate(kungFuBeat, transform.position, Quaternion.identity);
            beatsPool.Add(instance);
            instance.GetComponent<KungFuBeat>().SetSpawnPosition(transform);
            instance.gameObject.SetActive(false);
            instance.GetComponent<KungFuBeat>().SetInit();
            
        }

        nextBeatToSpawn = 0;
        headBeat = 0;

    }

    private void Start()
    {
        TempoReceiver.Instance.OnBeatReceived += UpdateHeadBeat;
        TempoReceiver.Instance.OnBeatMiss += DestroyHeadbeat;
        TryStartGenerate();
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
        _timeUtilNextBeat = beatPerSecond;

        if(remainBeatCount == 0)
        {
            isGenerating = false;
        }

    }

    private void SpawnBeat()
    {
        beatsPool[nextBeatToSpawn].gameObject.SetActive(true);
        nextBeatToSpawn++;
        nextBeatToSpawn = nextBeatToSpawn % (numBeatsInPool);
        
    }

    private void DestroyHeadbeat()
    {
        Debug.Log("Destroy Head beat");
        beatsPool[headBeat].gameObject.SetActive(false);
        headBeat += 1;
        headBeat = headBeat % (numBeatsInPool);

    }

    private void UpdateHeadBeat()
    {
        Debug.Log("Update head beat");
        headBeat += 1;
        headBeat = headBeat % (numBeatsInPool - 1);
    }

    
    public void TryStartGenerate()
    {
        if(isGenerating)
        {
            Debug.LogWarning("Already generating");
            return;
        }
        
        isGenerating = true;
        beatPerSecond = BPM / 60.0f;
       
        remainBeatCount = numOfBeatsToGenerate;
        _timeUtilNextBeat = beatPerSecond;
     
    }
}
