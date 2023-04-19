using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public abstract class KungFu : MonoBehaviour
{
    public float spawnInterval = 2f; // The interval between each spawn
    protected float timeUntilNextSpawn;
    protected Stance.stance _stance;

    [SerializeField] private LayerMask enemyLayer;
    protected float autoDashDistance = 1.0f;


    protected virtual void Start()
    {
        Stance.Instance.onSwitchStance += TryEnable;
        timeUntilNextSpawn = spawnInterval;
        enabled = false;
        TempoReceiver.Instance.OnAutoDash += DoAutoDash;
        TempoReceiver.Instance.OnAutoAttack += DoAutoAttack;
        
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
            //Perform();
        }
    }
    protected virtual void DoAutoDash()
    {
        if (enabled)
        {
            //Debug.Log("Do Auto Dash");
            
            transform.position = GetOptimalDashPosition();
        }
    }
    protected virtual void DoAutoAttack()
    {
        if (enabled)
        {
            //Debug.Log("Do Auto Attack");
            Perform();
        }
    }

    public abstract void Perform();


    private Vector3 GetOptimalDashPosition()
    {
        float checkAngleStep = 10f;

        Vector2 currentPosition = transform.position;
        Vector2 optimalDashPosition = currentPosition;
        float maxDistanceToClosestEnemy = Mathf.NegativeInfinity;

        for (float angle = 0; angle < 360; angle += checkAngleStep)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            Vector2 potentialDashPosition = currentPosition + direction * autoDashDistance;

            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(potentialDashPosition, autoDashDistance, enemyLayer);

            if (enemiesInRange.Length > 0)
            {
                float minDistanceToEnemy = Mathf.Infinity;

                foreach (Collider2D enemyCollider in enemiesInRange)
                {
                    float distanceToEnemy = Vector2.Distance(potentialDashPosition, enemyCollider.transform.position);
                    minDistanceToEnemy = Mathf.Min(minDistanceToEnemy, distanceToEnemy);
                }

                if (minDistanceToEnemy > maxDistanceToClosestEnemy)
                {
                    maxDistanceToClosestEnemy = minDistanceToEnemy;
                    optimalDashPosition = potentialDashPosition;
                }
            }
            else
            {
                return potentialDashPosition;
            }
        }

        return (Vector3)optimalDashPosition;
    }
}
