using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : Singleton<GameManager>
{
   
    private void Initialization()
    {
        Player.Instance.playerHurtBox.OnPlayerReceiveDmg += OnPlayerDeath;
    }
    
    private void Start()
    {
        Initialization();
    }

    public override void Awake()
    {
        base.Awake();
        LoadPrefabsFromResources();
        //Application.targetFrameRate = -1;
    }

    //----------Game Assets------------------------
  
    [HideInInspector] public GameObject pfDamagePopup;
    [HideInInspector] public GameObject pfTextPopup;
    private void LoadPrefabsFromResources()
    {
        
        pfDamagePopup = Resources.Load<GameObject>("pfDamagePopup");
        pfTextPopup = Resources.Load<GameObject>("pfTextPopup");
        //Debug.Log(pfDamagePopup);
        //Debug.Log(pfTextPopup);
    }

    private void OnPlayerDeath()
    {
        if(Player.Instance.playerHurtBox.GetHitPoints() <= 0)
        {
            //Player Death
            DG.Tweening.DOTween.KillAll();
            SceneManager.LoadScene("Retry Scene");
        }
    }
}
