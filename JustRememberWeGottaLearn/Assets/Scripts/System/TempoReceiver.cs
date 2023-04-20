using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoReceiver : Singleton<TempoReceiver>
{
    //public Action OnAutoAttack;
    //public Action OnAutoDash;
    public Action OnBeatReceived;
    

    //public Action<Vector3, string> OnMissTextPopup;

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
        //OnMissTextPopup += (Vector3 position, string text) => { TextPopup.Create(position, text); };
        Player.Instance.OnPlayerAttack += DoAttackBeatReceive;
        Player.Instance.OnPlayerDash += DoDashBeatReceive; 

    }
    private void Update()
    {
     
      

    }

    private void DoAttackBeatReceive()
    {
        if (hasBeat)
        {
            hasBeat.GetComponent<KungFuBeat>().Hit();
            hasBeat = null;
            OnBeatReceived.Invoke();
        }
        else
        {
            OnBeatReceived.Invoke();

        }

    }

    private void DoDashBeatReceive()
    {
        if (hasBeat)
        {
            hasBeat.GetComponent<KungFuBeat>().Hit();
            hasBeat = null;
            OnBeatReceived.Invoke();

        }
        else
        {
            OnBeatReceived.Invoke();
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
   
                hasBeat = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KungFuBeat"))
        {
   
            if(collision.gameObject != hasBeat)
            {
                //Debug.LogError("game object not equal to beat");
            }
            //hasBeat.SetActive(false);
            hasBeat = null;
            OnBeatReceived.Invoke();

        }
    }

}
