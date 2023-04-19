using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoReceiver : Singleton<TempoReceiver>
{
    public Action OnAutoAttack;
    public Action OnAutoDash;
    public Action OnBeatReceived;
    public Action OnBeatMiss;

    public Action<Vector3, string> OnMissTextPopup;

    private GameObject hasBeat;
    [SerializeField] private KeyCode autoAttackKey = KeyCode.J;
    [SerializeField] private KeyCode autoDashKey = KeyCode.K;


    public override void Awake()
    {
        base.Awake();
        hasBeat = null;
    }

    public void Start()
    {
        OnMissTextPopup += (Vector3 position, string text) => { TextPopup.Create(position, text); };


    }
    private void Update()
    {
       
        if (Input.GetKeyDown(autoAttackKey))
        {
            if (hasBeat)
            {
                hasBeat.GetComponent<KungFuBeat>().Hit();
                hasBeat.SetActive(false);
                OnAutoAttack.Invoke();
                hasBeat = null;
                OnBeatReceived.Invoke();

            }
            else
            {
               
                //To Do, destroy the head beat, and its a miss
                OnBeatMiss.Invoke();
                OnMissTextPopup.Invoke(transform.position, "Miss");
            }
        }
        else if (Input.GetKeyDown(autoDashKey))
        {
            if (hasBeat)
            {
                hasBeat.GetComponent<KungFuBeat>().Hit();
                hasBeat.SetActive(false);
                OnAutoDash.Invoke();
                hasBeat = null;
                OnBeatReceived.Invoke();

            }
            else
            {
                //Todo, destroy the head beat, and its a miss;
                OnBeatMiss.Invoke();
                OnMissTextPopup.Invoke(transform.position, "Miss");
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("KungFuBeat"))
        {
            if (hasBeat)
            {
                Debug.LogError("Two beat can not exist in Tempo zone at the same time");
            }
            else
            {
                //Debug.Log("Enter check zone");
                //Destroy(collision.gameObject);
                
                hasBeat = collision.gameObject;
                //OnBeatReceived.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KungFuBeat"))
        {
            //Debug.Log("Exit check zone");
            //Debug.Log(collision.gameObject);
            //Destroy the kung fu beat when it entered and exited from the check zone
            //Destroy(collision.gameObject);
            if(collision.gameObject != hasBeat)
            {
                Debug.LogError("game object not equal to beat");
            }
            hasBeat.SetActive(false);
            hasBeat = null;
            OnBeatReceived.Invoke();

        }
    }

}
