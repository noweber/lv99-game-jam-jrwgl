using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAbility : Card
{
    [SerializeField] private Stance.stance m_stance;

    protected override void PerformUpgrade()
    {
        switch (m_stance)
        {
            case Stance.stance.ShaolinKungFu:
                Player.Instance.GetComponent<Shaolin>()?.DoUpgrade();
                break;
            case Stance.stance.BruceLee:
                Player.Instance.GetComponent<BruceLeeKick>()?.DoUpgrade();
                break;
        }
    }
}
