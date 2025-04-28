using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float Mass;
    public float InitialForce;
    public float Acceleration;
    public float Mu;
    public float Angle;

    [SerializeField] protected LayerMask _platformLayer;

    protected float _time;

    protected Vector3 _velocity;
    [SerializeField] protected bool _canMove = false;

    protected Coroutine _moveRoutine;

    public bool CanMove => _canMove;

    public float Time_ { get; protected set; }
    public float Force { get; protected set; }
    public Vector3 Velocity { get; protected set; }
    public float A { get; protected set; }
    public float Friction { get; protected set; }

    public void Restart()
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);

        _velocity = Vector3.zero;
        _time = 0f;
        _canMove = true;
        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
    }

    protected IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return null;

            Move();
        }
    }

    protected virtual void Move()
    {
        if (!_canMove || Mass <= 0) return;

        float force = InitialForce + Acceleration * _time;

        float mu = 1;
        RaycastHit[] hit = Physics.RaycastAll(transform.position, -transform.up, 10, _platformLayer);
        if (hit.Length > 0) mu = hit[0].collider.GetComponent<Platform>().Mu;

        float angleRad = Angle * Mathf.Deg2Rad;
        float g = 9.81f;
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