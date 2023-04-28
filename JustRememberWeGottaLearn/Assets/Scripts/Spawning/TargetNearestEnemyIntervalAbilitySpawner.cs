using Assets.Scripts.Abilities;
using UnityEngine;

namespace Assets.Scripts.Spawning
{
    public sealed class TargetNearestEnemyIntervalAbilitySpawner : GameObjectIntervalSpawner
    {
        protected override void SpawnObject()
        {
            var enemyTarget = AbilityFunctions.GetNearestEnemy(gameObject);
            Instantiate(ObjectToSpawn, GetSpawnPosition(), Quaternion.identity).GetComponent<Rigidbody2dTargetChaser>().Initialize(enemyTarget, false);
        }
    }
}
