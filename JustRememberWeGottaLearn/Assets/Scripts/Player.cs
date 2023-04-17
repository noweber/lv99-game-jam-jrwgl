using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public AbilitySpawner abilitySpawner;

    private void Awake()
    {
        abilitySpawner = GetComponent<AbilitySpawner>();
    }
}
