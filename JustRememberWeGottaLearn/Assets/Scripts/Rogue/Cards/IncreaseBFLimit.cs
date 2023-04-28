using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class IncreaseBFLimit : Card
{
    [SerializeField] private int m_increaseBFAmount;

    protected override void PerformUpgrade()
    {
        TempoSystem.Instance.IncrementBFLimit(m_increaseBFAmount);
    }
}
