using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private Transform _startLaserPoint;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private EdgesCreator _edgesCreator;

    private float _previousRefractive = 1;

    private void Start()
    {
        ChangeAngle(0);
    }

    public void ChangeAngle(float value)
    {
        _angle = value;

        transform.rotation = Quaternion.Euler(0f, 0f, _angle);

        _edgesCreator.EnableColliders();

        _laser.positionCount = 1;
        _laser.SetPosition(0, _startLaserPoint.position);

        ContinueLaser(_startLaserPoint.position, _angle);
    }

    private void ContinueLaser(Vector2 startPos, float angle)
    {
        if (_laser.positionCount > 10) return;

        _laser.positionCount += 1;

        var pointIndex = _laser.positionCount - 1;
        var formattedAngle = (angle - 90) * Mathf.Deg2Rad;
        var direction = new Vector2(Mathf.Cos(formattedAngle), Mathf.Sin(formattedAngle));
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 100, _layerMask);

        if (hit.collider != null)
        {
            _laser.SetPosition(pointIndex, hit.point);

            var edge = hit.collider.GetComponent<Edge>();
            edge.SetEnabledCollider(false);

            var newAngle = angle;
            if (edge.Refractive >= 1) 
                newAngle = Mathf.Asin(Mathf.Sin(angle * Mathf.Deg2Rad) / edge.Refractive) * _previousRefractive * Mathf.Rad2Deg;

            _previousRefractive = edge.Refractive;

            ContinueLaser(hit.point, newAngle);
        }
        else
        {
            _laser.SetPosition(pointIndex, direction * 100 + startPos);
        }
    }
}
