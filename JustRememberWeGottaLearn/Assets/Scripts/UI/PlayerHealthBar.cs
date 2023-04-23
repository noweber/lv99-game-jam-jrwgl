using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    Transform playerTransform;
    private Vector3 m_playerLastPosition;
    private void Start()
    {
        playerTransform = Player.Instance.transform;
    }

    private void LateUpdate()
    {
        if (playerTransform)
        {
            Vector3 targetPosition = transform.position + (playerTransform.position - m_playerLastPosition);
            m_playerLastPosition = playerTransform.position;
            transform.position = targetPosition;

        }
    }
}
