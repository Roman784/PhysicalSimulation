using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 _position;
    private float _mass;
    private Vector2 _velocity;

    public float Mass => _mass;
    public Vector2 Velocity => _velocity;
    public Vector2 Position => _position;

    public event Action<Vector2> OnCollided;

    private void Start()
    {
        _position = transform.position;
    }

    public void SetMass(float mass)
    {
        _mass = mass;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }

    private void Update()
    {
        _position += _velocity * Time.deltaTime;
        transform.position = _position;
    }

    public void InvokeOnCollided()
    {
        OnCollided?.Invoke(_velocity);
    }
}
