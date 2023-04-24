using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBarUI : MonoBehaviour
{

    private Slider healthBar; 
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = 10.0f;
        healthBar.value = Player.Instance.playerHurtBox.GetHitPoints();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = Player.Instance.transform.position;
        healthBar.value = Player.Instance.playerHurtBox.GetHitPoints();
    }
}
