using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoSystem : Singleton<TempoSystem>
{
    // Start is called before the first frame update
    Transform playerTransform;
    Vector3 playerLastPosition;
    void Start()
    {
        playerTransform = Player.Instance.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
