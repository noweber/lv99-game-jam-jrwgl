using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreHP : Card
{
    [SerializeField] private int m_HpAmount = 5;

    protected override void PerformUpgrade()
    {
        Debug.Log("Add HP by: " + m_HpAmount.ToString());
        Player.Instance.playerHurtBox.IncreaseMaxHP(m_HpAmount);
    }

}
