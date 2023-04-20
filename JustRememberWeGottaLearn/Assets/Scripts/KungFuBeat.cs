using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class KungFuBeat : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 5;
    private bool isHit;
    private Transform spawnPosition;
    private bool isInit;

    public Action<Vector3, string> OnMissTextPopup;


    public Action OnBeatMiss;
    public Action OnBeatHit;

    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Awake()
    {
        isHit = false;
        isInit = false;
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        OnMissTextPopup += (Vector3 position, string text) => { TextPopup.Create(position, text); };
        TempoGenerator.Instance.OnBpmChange += DoChangeBeatColor;
    }

    private void DoChangeBeatColor(BPM bpm)
    {
        if(bpm == BPM.bpm180plus)
        {
            Debug.Log("Do change color");
            _sprite.color = Color.red;
        }
        else
        {
            _sprite.color = Color.white;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * movingSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        TempoGenerator.Instance.OnBpmChange -= DoChangeBeatColor;
    }
    private void OnEnable()
    {
        TempoGenerator.Instance.OnBpmChange += DoChangeBeatColor;
        isHit = false;

    }

    public void Hide()
    {
        //Debug.Log("Beat is disabled and moved back to the spawn location ");
        Vector3 missPosition = transform.position;
        if (spawnPosition)//Clean up code
            transform.position = spawnPosition.position;

        if (!isHit)
        {
            //Debug.Log("To do, miss beat text pop up");
            OnBeatMiss.Invoke();
            //OnMissTextPopup?.Invoke(missPosition, "Miss");
        }
        else
        {
            OnBeatHit.Invoke();
            OnMissTextPopup?.Invoke(missPosition, "Breath");
        }
        gameObject.SetActive(false);

    }

    
    public void Hit()
    {
        isHit = true;
    }
    public void SetSpawnPosition(Transform _spawnPosition)
    {
        spawnPosition = _spawnPosition;
    }
}
