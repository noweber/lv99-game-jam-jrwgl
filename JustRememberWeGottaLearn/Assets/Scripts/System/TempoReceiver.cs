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

    //private GameObject hasBeat;
    private List<KungFuBeat> beats = new List<KungFuBeat>();
    [SerializeField] private KeyCode autoAttackKey = KeyCode.J;
    [SerializeField] private KeyCode autoDashKey = KeyCode.K;


    public override void Awake()
    {
        base.Awake();
        //hasBeat = null;
    }

    public void Start()
    {
        //OnMissTextPopup += (Vector3 position, string text) => { TextPopup.Create(position, text); };
        Player.Instance.OnPlayerAttack += DoAttackBeatReceive;
        Player.Instance.OnPlayerDash += DoDashBeatReceive; 

    }
   
    private void DoAttackBeatReceive()
    {
        //if (hasBeat)
        if(beats.Count != 0)
        {
            //hasBeat.GetComponent<KungFuBeat>().Hit();
            //hasBeat = null;
            KungFuBeat beat = beats[0];
            //beats.RemoveAt(0);
            beat.Hit();
            OnBeatReceived.Invoke();
        }
        else
        {
            OnBeatReceived.Invoke();

        }

    }

    private void DoDashBeatReceive()
    {
        if (beats.Count != 0)
        {
            //hasBeat.GetComponent<KungFuBeat>().Hit();
            //hasBeat = null;
            KungFuBeat beat = beats[0];
            //beats.RemoveAt(0);
            beat.Hit();
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
            /*
            if (hasBeat)
            {
                Debug.LogError("Two beat can not exist in Tempo zone at the same time");
            }
            else
            {
   
                hasBeat = collision.gameObject;
            }
            */

            beats.Add(collision.gameObject.GetComponent<KungFuBeat>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("KungFuBeat"))
        {
            /*
            if(collision.gameObject != hasBeat)
            {
                //Debug.LogError("game object not equal to beat");
            }
            //hasBeat.SetActive(false);
            hasBeat = null;
            OnBeatReceived.Invoke();
            */
            beats.RemoveAt(0);
            OnBeatReceived.Invoke();
        }
    }

}
