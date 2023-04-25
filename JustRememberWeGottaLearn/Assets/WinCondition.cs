using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public List<EnemySpawner> spawners;

    private void Update()
    {
        bool isWin = true;
        for (int i = 0; i < spawners.Count; i++)
        {
            if (!spawners[i].isWin)
            {
                isWin = false;
                break;
            }

            if (isWin)
            {
                SceneManager.LoadScene("Win Scene");
            }
        }
    }
}
