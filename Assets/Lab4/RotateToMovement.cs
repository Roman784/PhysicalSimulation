using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotateToMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Vector3 _previousPosition;

    private void Update()
    {
        if (transform.position == _previousPosition) return;

        Vector3 direction = transform.position - _previousPosition;
        _previousPosition = transform.position;

        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation *= Quaternion.Euler(0, 0, -90);
        transform.rotation = rotation;
    }
}
