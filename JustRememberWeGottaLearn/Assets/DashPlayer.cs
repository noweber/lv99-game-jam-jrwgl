using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashPlayer : MonoBehaviour
{

    public Transform playerTransform;

    private void Awake()
    {

        GetComponent<Button>().onClick.AddListener(Dash);
    }

    private void Dash()
    {
        Vector3 dashDir = Vector3.zero; //=  DashEnemyTest.Instance.calculateDash(playerTransform.position);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerTransform.position, 5.0f);
        Debug.Log(colliders.Length);
        float distanceSum = 0;
        List<Vector3> vs = new List<Vector3>();
        List<float> dists = new List<float>();
        foreach(var c in colliders)
        {
            Vector3 dir = c.transform.position - playerTransform.position;
            dists.Add(dir.magnitude);
            vs.Add(dir.normalized);
            distanceSum += dir.magnitude;
        }
        
        for(int i = 0; i < vs.Count; i++)
        {
            dashDir = -vs[i] * (dists[i] / distanceSum);
        }
        dashDir = new Vector3(dashDir.x, dashDir.y, 0);

        playerTransform.position = playerTransform.position + dashDir.normalized * 3 ;
        Debug.Log(dashDir);
        
    }

}
