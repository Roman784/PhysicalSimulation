using UnityEngine;
using Utils;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private Transform _startLaserPoint;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _airLayerMask;
    [SerializeField] private Reflaction _reflactionPrefab;

    private ObjectPool<Reflaction> _reflectionsPool;

    private void Start()
    {
        _reflectionsPool = new(_reflactionPrefab, 10);

        ChangeAngle(-90 * Mathf.Deg2Rad);
    }

    public void ChangeAngle(float value)
    {
        _angle = value;

        transform.rotation = Quaternion.Euler(0f, 0f, _angle * Mathf.Rad2Deg - 90);

        foreach (var reflection in _reflectionsPool.GetInstances())
            _reflectionsPool.ReleaseInstance(reflection);

        _laser.positionCount = 1;
        _laser.SetPosition(0, _startLaserPoint.position);

        ContinueLaser(_startLaserPoint.position, _angle, _layerMask, 1f);
    }

    private void ContinueLaser(Vector2 startPos, float angle, LayerMask layerMask, float prevRefractive)
    {
        if (_laser.positionCount > 15) return;
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
                var angleBetweenNormalAndDirection = Vector2.Angle(-direction, normal) * Mathf.Deg2Rad;
                var sin = Mathf.Sin(angleBetweenNormalAndDirection) * prevRefractive / refractive;

                if (Mathf.Abs(sin) > 1)
                {
                    CreateReflection(hit, direction);
                    return;
                }

                var newAngle = Mathf.Asin(sin);

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

    private void CreateReflection(RaycastHit2D rayHit, Vector2 rayDirection)
    {
        var direction = Vector2.Reflect(rayDirection, rayHit.normal);
        var startPosition = rayHit.point + direction * 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, 100, _airLayerMask);
        if (hit.collider != null)
        {
            _reflectionsPool.GetInstance().ThrowRay(rayHit.point, hit.point);
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
