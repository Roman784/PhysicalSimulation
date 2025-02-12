using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Bird : MonoBehaviour
{
    public static Bird Instance;

    private Vector3 _speed;
    private Vector3 _acceleration;
    private Vector3 _startPosition;

    private MoveType _moveType;
    private float _time = 0f;
    private bool _canMove = false;

    public UnityEvent<float> OnTimeChanged = new();
    public UnityEvent<float> OnPathChanged = new();
    public UnityEvent<Vector3> OnSpeedChanged = new();
    public UnityEvent<Vector3> OnPositionChanged = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Vector3 position = Vector3.zero;
        float path = 0f;

        if (_moveType == MoveType.UniformMotion)
        {
            var moveResult = MoveUniformly(_speed, _startPosition, _time);
            position = moveResult.Item1;
            path = moveResult.Item2;
        }
        else if (_moveType == MoveType.UniformlyAcceleratedMotion)
        {
            var moveResult = MoveUniformlyAccelerated(_speed, _acceleration, _startPosition, _time);
            position = moveResult.Item1;
            path = moveResult.Item2;
            Vector3 speed = moveResult.Item3;

            OnSpeedChanged.Invoke(speed);
        }

        if (position.y < 0) position.y = 0;
        transform.position = position;

        IncreaseTime();

        OnPositionChanged.Invoke(position);
        OnPathChanged.Invoke(path);
    }

    public void ChangeSpeed(Vector3 newSpeed) => _speed = newSpeed;
    public void ChangeAcceleration(Vector3 newAcceleration) => _acceleration = newAcceleration;
    public void ChangeStartPosition(Vector3 newStartPosition) => _startPosition = newStartPosition;
    public void ResetTime() => _time = 0;
    public void SetCanMove(bool canMove) => _canMove = canMove;
    public void SetMoveType(MoveType type) => _moveType = type;

    private void IncreaseTime()
    {
        if (!_canMove) return;

        _time += Time.deltaTime;
        OnTimeChanged.Invoke(_time);
    }

    private (Vector3, float) MoveUniformly(Vector3 speed, Vector3 startPosition, float time)
    {
        Vector3 path = speed * time;
        Vector3 position = startPosition + path;

        return (position, CalculatePath(path));
    }

    private (Vector3, float, Vector3) MoveUniformlyAccelerated(Vector3 startSpeed, Vector3 acceleration, Vector3 startPosition, float time)
    {
        Vector3 speed = startSpeed + acceleration * time;
        Vector3 position = startPosition + startSpeed * time + acceleration * Mathf.Pow(time, 2) / 2f;
        Vector3 path = position - startPosition;

        return (position, CalculatePath(path), speed);
    }

    private float CalculatePath(Vector3 path)
    {
        return CalculatePath(path, Vector3.zero);
    }

    private float CalculatePath(Vector3 startPosition, Vector3 endPosition)
    {
        float x = Mathf.Pow(endPosition.x - startPosition.x, 2);
        float y = Mathf.Pow(endPosition.y - startPosition.y, 2);
        float z = Mathf.Pow(endPosition.z - startPosition.z, 2);

        return Mathf.Sqrt(x + y + z);
    }
}
