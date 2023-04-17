using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QiBar : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public void Update()
    {
        if (player != null)
            GetComponent<Slider>().value = player.GetComponent<SideScrollerPlayerController>().getQi();
    }
}
