using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KungFuBeat : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 5;
    private bool isHit;
    private Transform spawnPosition;
    private bool isInit;

    public Action<Vector3, string> OnMissTextPopup;
    public Stance.stance _stance;
        
    // Start is called before the first frame update
    void Awake()
    {
        isHit = false;
        isInit = false;
    }

    private void Start()
    {
        OnMissTextPopup += (Vector3 position, string text) => { TextPopup.Create(position, text); };
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * movingSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        if (isInit)
        {
            //Debug.Log("Beat is disabled and moved back to the spawn location ");
            Vector3 missPosition = transform.position;
            transform.position = spawnPosition.position;
            if (!isHit)
            {
                //Debug.Log("To do, miss beat text pop up");
                OnMissTextPopup.Invoke(missPosition, "Miss");
            }
            else
            {
                OnMissTextPopup.Invoke(missPosition, "Hit");
            }
        }

    }

    private void OnEnable()
    {
        isHit = false;
        
    }
    public void Hit()
    {
        isHit = true;
    }

    public void Init()
    {
        isInit = true;
    }

    public void SetSpawnPosition(Transform _spawnPosition)
    {
        spawnPosition = _spawnPosition;
    }
}
