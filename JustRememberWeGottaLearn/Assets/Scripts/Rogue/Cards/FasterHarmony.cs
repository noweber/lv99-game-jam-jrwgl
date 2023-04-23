using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterHarmony : Card
{
    protected override void PerformUpgrade()
    {
        TempoSystem.Instance.DecrementHarmonyHitCount();
    }
}
