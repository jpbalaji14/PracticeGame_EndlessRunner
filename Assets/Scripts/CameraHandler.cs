using TMPro;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform Target;
   
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private Vector3 _camPositionOnBaseSpeed;
    [SerializeField] private Vector3 _camPositionOnBoostSpeed;
    private Vector3 _targetPosition;
    private Vector3 _velocity = Vector3.zero;
    private void Start()
    {
        _targetPosition = Target.TransformPoint(_camPositionOnBaseSpeed);
    }
    void Update()
    {
        // Smoothly move the camera towards that target positions
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition,ref _velocity, _smoothTime);

        if (GameManager.Instance.CharacterHandler.IsPlayerBoostActivated)
        {
            // Define a target position above and behind the target transform
            _targetPosition = Target.TransformPoint(_camPositionOnBoostSpeed);
        }
        else
        {
            // Define a target position above and behind the target transform
             _targetPosition = Target.TransformPoint(_camPositionOnBaseSpeed);
        }

    }
}
