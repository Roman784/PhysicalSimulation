using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Block1 : Block
{
    [Space]

    [SerializeField] private LineRenderer _line;
    [SerializeField] public Transform _linePoint1;
    [SerializeField] public Transform _linePoint2;
    [SerializeField] public Transform _linePoint3;

    [Space]

    [SerializeField] private Block _block2;

    private void Update()
    {
        _line.SetPosition(0, _linePoint1.position);
        _line.SetPosition(1, _linePoint2.position);
        _line.SetPosition(2, _linePoint3.position);
    }

    protected override void Move()
    {
        if (!_canMove || Mass <= 0) return;

        float g = 9.81f;
        float force = InitialForce + Acceleration * _time + _block2.Mass * g;

        float mu = 1;
        RaycastHit[] hit = Physics.RaycastAll(transform.position, -transform.up, 10, _platformLayer);
        if (hit.Length > 0) mu = hit[0].collider.GetComponent<Platform>().Mu;

        float angleRad = Angle * Mathf.Deg2Rad;
        float N = Mass * g * Mathf.Cos(angleRad);
        float gravityForce = Mass * g * Mathf.Sin(angleRad);
        float frictionForce = mu * N;

        force -= gravityForce;
        float resForce = force - frictionForce * Mathf.Sign(force);

        float a = resForce / Mass;

        if (force < 0 && a > 0 && _velocity.x >= 0 || force > 0 && a < 0 && _velocity.x <= 0)
        {
            Debug.Log("1");
            _velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("2");
            _velocity += transform.right * a * Time.deltaTime;
        }

        transform.position += _velocity * Time.deltaTime;
        _time += Time.deltaTime;

        Time_ = _time;
        Force = force;
        Friction = frictionForce;
        A = a;
        Velocity = _velocity;
    }
}