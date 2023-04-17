using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathButton : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Breath);
    }

    private void Breath()
    {
        if(Player != null)
            Player.GetComponent<SideScrollerPlayerController>().QiChange(10.0f);
    }
}
