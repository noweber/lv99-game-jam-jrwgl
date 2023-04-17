using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public abstract class KungFu : MonoBehaviour
{
    public float spawnInterval = 2f; // The interval between each spawn
    protected float timeUntilNextSpawn;
    protected Stance.stance _stance;
    protected virtual void Start()
    {
        Stance.Instance.onSwitchStance += TryEnable;
        timeUntilNextSpawn = spawnInterval;
        enabled = false;
    }

    protected void TryEnable()
    {
        if(_stance == Stance.Instance.currentStance)
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }
    }

    protected virtual void Update()
    {
        if (timeUntilNextSpawn > 0)
        {
            timeUntilNextSpawn -= Time.deltaTime;
        }
        if (timeUntilNextSpawn <= 0)
        {
            // Reset the timer
            timeUntilNextSpawn = spawnInterval;

            // Spawn the game object
            Perform();
        }
    }

    public abstract void Perform();
    
}
