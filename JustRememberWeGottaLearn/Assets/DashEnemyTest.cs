using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemyTest : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> enemies;

    private static DashEnemyTest _instance;

    private float timer = 5.0f;

    public void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            GameObject go = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemies.Add(go);
            timer = 5.0f;
        }

    }

    public static DashEnemyTest Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(DashEnemyTest)) as DashEnemyTest;
            }
            return _instance;
        }
    }

    public Vector3 calculateDash(Vector3 pos)
    {
        return Vector3.right;
    }

}
