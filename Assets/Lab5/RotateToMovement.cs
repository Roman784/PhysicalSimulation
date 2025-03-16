using UnityEngine;

public class RotateToMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Vector3 _previousPosition;

    private void Update()
    {
        if (transform.position == _previousPosition) return;

        Vector3 direction = (transform.position - _previousPosition).normalized;
        _previousPosition = transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        rotation = Quaternion.Euler(rotation.eulerAngles + _offset);
        transform.rotation = rotation;
    }
}
