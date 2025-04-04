using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleType _type;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    [SerializeField] private Color _color3;

    public float Mass { get; private set; } = 1000;
    public Vector2 Velocity { get; private set; }
    public ObstacleType Type => _type;

    public void Init(ObstacleType type)
    {
        _type = type;

        if (type == ObstacleType.Static)
        {
            _spriteRenderer.color = _color1;
            Mass = 100000;
        }
        else if (type == ObstacleType.Movable)
        {
            _spriteRenderer.color = _color2;
            Mass = 10f;
        }
        else if (type == ObstacleType.Booster)
        {
            _spriteRenderer.color = _color3;
            Mass = 100000;
        }
    }

    public void SetMass(float mass)
    {
        Mass = mass;
    }

    public void SetVelocity(Vector2 velocity)
    {
        Debug.Log(velocity);
        Velocity = velocity;
    }

    private void Update()
    {
        transform.position += (Vector3)Velocity * Time.deltaTime;
    }

    public enum ObstacleType
    {
        Static,
        Movable,
        Booster
    }
}
