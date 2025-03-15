using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoveableObject : MonoBehaviour
{
    public static MoveableObject Instance;

    [SerializeField] private float _initialVelocity;
    [SerializeField] private float _initialAcceleration;
    [SerializeField] private float _jerk;
    [SerializeField] private float _radius;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private float _t;
    private float _t1;

    private float _time = 0f;
    private bool _canMove = false;

    private Coroutine _moveRoutine;

    public UnityEvent<float> OnTimeChanged = new();
    public UnityEvent<float> OnPathChanged = new();
    public UnityEvent<float> OnVelocityChanged = new();
    public UnityEvent<float> OnAccelerationChanged = new();
    public UnityEvent<Vector3> OnPositionChanged = new();
    public UnityEvent<Vector3> OnT1PositionChanged = new();
    public UnityEvent OnTimeReseted = new();

    public float MovementTime => Mathf.Clamp(_time - _t, 0, _time);
    public float InitialVelocity => _initialVelocity;
    public float InitialAcceleration => _initialAcceleration;
    public float Jerk => _jerk;
    public float Radius => _radius;
    public float Velocity => Formulas.Velocity(InitialVelocity, InitialAcceleration, Jerk, MovementTime);
    public float Acceleration => Formulas.Acceleration(InitialAcceleration, Jerk, MovementTime);
    public Vector3 Position => ClampPosition(Formulas.Position(_radius, _initialVelocity, _initialAcceleration, _jerk, MovementTime));
    public Vector3 T1Position => ClampPosition(Formulas.Position(_radius, _initialVelocity, _initialAcceleration, _jerk, _t1));
    public float Path => Formulas.Path(InitialVelocity, InitialAcceleration, Jerk, Radius, MovementTime);

    private void Awake()
    {
        Instance = this;
        _canMove = false;

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public void SetTime(Vector2 time)
    {
        _t = time.x;
        _t1 = time.y;
        ResetTime();
    }

    public void SetInitialVelocity(float velocity)
    {
        _initialVelocity = velocity;
        UpdateExitData();
        ResetTime();
    }

    public void SetInitislAcceleration(float acceleration)
    {
        _initialAcceleration = acceleration;
        UpdateExitData();
        ResetTime();
    }

    public void SetJerk(float jerk)
    {
        _jerk = jerk;
        UpdateExitData();
        ResetTime();
    }

    public void SetRadius(float radius)
    {
        _radius = radius;
        UpdateExitData();
        ResetTime();
    }

    public void ResetTime()
    {
        _time = 0;
        UpdateExitData();
        OnTimeReseted.Invoke();

        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
            _moveRoutine = null;
            if (_canMove)
                _moveRoutine = StartCoroutine(Move());
        }

        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }

    private void UpdateExitData()
    {
        OnTimeChanged?.Invoke(_time);
        OnPathChanged?.Invoke(Path);
        OnVelocityChanged?.Invoke(Velocity);
        OnAccelerationChanged?.Invoke(Acceleration);
        OnT1PositionChanged?.Invoke(T1Position);
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;

        if (_canMove && _moveRoutine == null)
            _moveRoutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return new WaitUntil(() => _time < _t);

        while (true)
        {
            transform.position = Position;
            OnPositionChanged?.Invoke(Position);
            UpdateExitData();

            yield return null;
        }
    }

    private void Update()
    {
        IncreaseTime();
    }

    private void IncreaseTime()
    {
        if (!_canMove) return;

        _time += Time.deltaTime;
        OnTimeChanged?.Invoke(_time);
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        position.y = Mathf.Clamp(position.y, 0, position.y);
        return position;
    }
}
