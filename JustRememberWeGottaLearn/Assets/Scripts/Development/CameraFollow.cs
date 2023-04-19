using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 _playerLastPosition;
    Transform playerTransform;

    private void Start()
    {
        
        playerTransform = Player.Instance.gameObject.transform;
        _playerLastPosition = playerTransform.position; 
    }
    private void LateUpdate()
    {
        transform.position += playerTransform.position - _playerLastPosition;
        _playerLastPosition = playerTransform.position;
    }
}