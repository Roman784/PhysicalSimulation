
using System.Collections;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public float Speed;
    public float Ratio;
    public float M;
    public float D;
    public float Z;

    public bool CanMove;
    public Vector3 Center;

    [SerializeField] private Gear _nextGear;

    private void Awake()
    {
        Center = transform.position;
    }

    public void UpdateDiametr()
    {
        var radius = D / 2f;

        transform.localScale = new Vector3(D, D, transform.localScale.z);

        if (_nextGear != null)
        {
            Vector3 directionToNext = (_nextGear.Center - Center).normalized;
            Vector3 newNextCenter = Center + directionToNext * (radius + _nextGear.D / 2f);
            _nextGear.Center = newNextCenter;
            _nextGear.transform.position = newNextCenter;

            _nextGear.UpdateDiametr();
        }
    }

    public IEnumerator UpdateRoutine()
    {
        while (CanMove)
        {
            yield return null;

            Move();
        }
    }

    private void Move()
    {
        transform.Rotate(0, 0, Speed * Time.deltaTime * Mathf.Rad2Deg);

        if (_nextGear == null) return;

        _nextGear.Speed = -Speed / Ratio;
        _nextGear.Move();
    }
}