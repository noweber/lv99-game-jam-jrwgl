using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class KungFuBeat : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 5;
    private Transform spawnPosition;
   
    private SpriteRenderer _sprite;
    private bool m_inReceiver;
    private bool m_playedSFX;

    // Start is called before the first frame update
    void Awake()
    {
        m_inReceiver = false;
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //TempoGenerator.Instance.OnBpmChange += DoChangeBeatColor;
        TempoSystem.Instance.OnBpmChange += DoChangeBeatColor;
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
  
    public void Hide()
    {
        if (spawnPosition)//Clean up code
            transform.position = spawnPosition.position;


        m_inReceiver = false;
        m_playedSFX = false;
        gameObject.SetActive(false);

    }

    public void SetSpawnPosition(Transform _spawnPosition)
    {
        spawnPosition = _spawnPosition;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.right * movingSpeed * Time.deltaTime;
        if (transform.position.x > TempoReceiver.Instance.LeftDetectionPointX && transform.position.x < TempoReceiver.Instance.RightDetectionPointX && !m_inReceiver)
        {
            m_inReceiver = true;
            TempoReceiver.Instance.AddMe(this);
        }

        if (transform.position.x > TempoReceiver.Instance.LeftDetectionPointX - 0.05f * movingSpeed && !m_playedSFX)
        {
            Debug.Log("Player breating SFX");
            m_playedSFX = true;
            AudioManager.instance.RequestSFX(SFXTYPE.breath);
        
        }
    }
}
