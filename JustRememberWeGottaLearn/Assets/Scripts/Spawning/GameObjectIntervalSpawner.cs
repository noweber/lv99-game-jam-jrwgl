﻿using System.Collections;
using UnityEngine;

public class GameObjectIntervalSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject ObjectToSpawn;
    [SerializeField] protected float SpawnInterval = 2f;
    [SerializeField] protected bool ShouldSpawnObjects = true;
    [SerializeField] protected Vector2Int SpawnOffset = Vector2Int.zero;

    protected virtual void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    protected virtual IEnumerator SpawnCoroutine()
    {
        while (ShouldSpawnObjects)
        {
            yield return new WaitForSeconds(GetSpawnInterval());
            SpawnObject();
        }
    }

    protected virtual void SpawnObject()
    {
        Instantiate(ObjectToSpawn, GetSpawnPosition(), Quaternion.identity);
    }

    protected virtual float GetSpawnInterval()
    {
        return SpawnInterval;
    }

    protected virtual Vector3 GetSpawnPosition()
    {
        return transform.position + new Vector3(SpawnOffset.x, SpawnOffset.y, 0);
    }
}
