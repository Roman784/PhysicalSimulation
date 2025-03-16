using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _archer;
    [SerializeField] private Transform _ground;
    [SerializeField] private Vector2 _heightOffset;

    private void Start()
    {
        MoveableObject.Instance.OnAngleChanged.AddListener(Rotate);
        MoveableObject.Instance.OnHeightChanged.AddListener(MoveUp);
    }

    private void Rotate(float angle)
    {
        _target.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void MoveUp(float height)
    {
        float archerY = height - _heightOffset.y;
        _archer.position = new Vector2(_archer.position.x, archerY);
    }
}
