using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private void Start()
    {
        Stance.Instance.switchStance += ActivateAbility;
    }
    private void ActivateAbility()
    {
        if(Stance.Instance.currentStance == Stance.stance.ShaolinKungFu)
        {
            Player.Instance.abilitySpawner.enabled = true;
        }
        else
        {
            Player.Instance.abilitySpawner.enabled = false;
        }
    }
}
