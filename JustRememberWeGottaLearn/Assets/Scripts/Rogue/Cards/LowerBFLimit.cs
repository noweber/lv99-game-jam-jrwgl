using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBFLimit : Card
{
    [SerializeField] private int m_lowerBFAmount = 10;
    protected override void PerformUpgrade()
    {
        TempoSystem.Instance.DecrementBFLimt(m_lowerBFAmount);
    }
}
