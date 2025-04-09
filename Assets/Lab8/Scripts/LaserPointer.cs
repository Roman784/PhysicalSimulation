using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private Transform _startLaserPoint;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _airLayerMask;
    [SerializeField] private EdgesCreator _edgesCreator;
    [SerializeField] private float _offset;
    [SerializeField] private Transform _direction;

    private void Start()
    {
        ChangeAngle(-90 * Mathf.Deg2Rad);
    }

    private void Update()
    {
        //Debug.Log(Mathf.Atan2(_direction.right.y, _direction.right.x) * Mathf.Rad2Deg);
        ChangeAngle(_angle);
    }

    public void ChangeAngle(float value)
    {
        _angle = value;

        transform.rotation = Quaternion.Euler(0f, 0f, _angle * Mathf.Rad2Deg - 90);

        _laser.positionCount = 1;
        _laser.SetPosition(0, _startLaserPoint.position);

        ContinueLaser(_startLaserPoint.position, _angle, _layerMask, 1f);
    }

    private void ContinueLaser(Vector2 startPos, float angle, LayerMask layerMask, float prevRefractive)
    {
        if (_laser.positionCount > 10) return;
        _laser.positionCount += 1;

        var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        startPos += direction * 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 100, layerMask);
        if (hit.collider != null)
        {
            _laser.SetPosition(_laser.positionCount - 1, hit.point);

            var refractive = hit.collider.GetComponent<RefractiveObject>()?.Refractive ?? 1f;
            if (refractive >= 1)
            {
                var normal = hit.normal;
                var angleNorDir = Vector2.Angle(-direction, normal) * Mathf.Deg2Rad;
                var normalAngle = Vector2.Angle(normal, Vector2.right) * Mathf.Deg2Rad;
                var sinTheta2 = Mathf.Sin(angleNorDir) * prevRefractive / refractive;

                if (Mathf.Abs(sinTheta2) > 1) return;

                var newAngle = Mathf.Asin(sinTheta2);

                Debug.Log($"dir: {direction} | nor: {normal}");

                newAngle *= Sign(direction, normal);
                newAngle += Mathf.Atan2(-normal.y, -normal.x);

                LayerMask nextLayerMask = (layerMask == _layerMask) ? _airLayerMask : _layerMask;
                ContinueLaser(hit.point, newAngle, nextLayerMask, refractive);
            }
        }
        else
        {
            _laser.SetPosition(_laser.positionCount - 1, startPos + direction * 100);
        }
    }

    private int Sign(Vector2 direction, Vector2 normal)
    {
        direction.Normalize();
        normal.Normalize();

        var direcctionAngle = Mathf.Atan2(-direction.y, -direction.x);
        var normalAngle = Mathf.Atan2(normal.y, normal.x);

        var offset = 90 * Mathf.Deg2Rad - normalAngle;
        direcctionAngle += offset;

        var newDirection = new Vector2(Mathf.Cos(direcctionAngle), Mathf.Sin(direcctionAngle));

        if (direcctionAngle > 0)
        {
            return newDirection.x < 0 ? 1 : -1;
        }
        else if (direcctionAngle < 0)
        {
            return newDirection.x > 0 ? 1 : -1;
        }

        return 0;
    }
}
