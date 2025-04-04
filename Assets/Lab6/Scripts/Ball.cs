using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform _arrow;

    [Space]

    private Vector2 _position;
    [SerializeField] private float _mass;
    [SerializeField] private Vector2 _velocity;
    private Vector2 _initialPosition;
    private bool _canMove;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _layerMask;

    public float Mass => _mass;
    public Vector2 Velocity => _velocity;
    public Vector2 Position => _position;

    public event Action<Vector2> OnVelocityChanged;

    private void Start()
    {
        _initialPosition = transform.position;
        _position = transform.position;

        _lineRenderer.positionCount = 10;

    }

    public void ResetPosition()
    {
        transform.position = _initialPosition;
        _position = transform.position;
    }

    public void SetMass(float mass)
    {
        _mass = mass;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
        OnVelocityChanged?.Invoke(_velocity);
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
        _arrow.gameObject.SetActive(!value);
        _lineRenderer.enabled = !value;
    }

    public void RotateArrow(float angle)
    {
        if (!_arrow.gameObject.activeSelf) return;

        _arrow.rotation = Quaternion.Euler(0, 0, angle);

        angle *= Mathf.Deg2Rad;
        var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        Vector2 startPosition = transform.position;
        Vector2 currentPosition = startPosition;
        Vector2 currentDirection = direction;

        _lineRenderer.positionCount = 10;
        _lineRenderer.SetPosition(0, currentPosition);

        for (int i = 1; i < 10; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentDirection, 100, _layerMask);
            if (hit.collider != null)
            {
                _lineRenderer.SetPosition(i, hit.point);

                currentPosition = hit.point - currentDirection * 0.1f;
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);

                if (hit.collider.tag == "APoint")
                {
                    _lineRenderer.positionCount = i+1;
                    break;
                }
            }
            else
            {
                _lineRenderer.SetPosition(10, currentPosition + currentDirection * 100);
            }
        }
    }

    private void Update()
    {
        if (!_canMove) return;

        _position += _velocity * Time.deltaTime;
        transform.position = _position;

        var angle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
        RotateArrow(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            new Simulation().HandleCollision(this, obstacle, collision.GetContact(0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "APoint")
            SetCanMove(false);
    }
}
