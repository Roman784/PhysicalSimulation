using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoveableObject : MonoBehaviour
{
    public static MoveableObject Instance;

    [SerializeField] private Vector2 _initialVelocity;
    [SerializeField] private Vector2 _initialAcceleration;
    [SerializeField] private float _angle;
    [SerializeField] private float _height;

    [Space]

    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _arrowSpawnPoint;
    private Transform _arrow;

    private float _t;

    private float _time = 0f;
    private bool _canMove = false;

    private Coroutine _moveRoutine;

    public UnityEvent<float> OnTimeChanged = new();
    public UnityEvent<float> OnPathChanged = new();
    public UnityEvent<float> OnFlightDurationChanged = new();
    public UnityEvent<float> OnFlightDistanceChanged = new();
    public UnityEvent<float> OnAverageVelocityChanged = new();
    public UnityEvent<float> OnLandingVelocityChanged = new();
    public UnityEvent<float> OnAngleChanged = new();
    public UnityEvent<float> OnHeightChanged = new();
    public UnityEvent OnTimeReseted = new();

    public float MovementTime => Mathf.Clamp(_time - _t, 0, _time);
    public Vector2 Position => Formulas.Position(_arrowSpawnPoint.position, _initialVelocity, _initialAcceleration, _angle, MovementTime);
    public float Path => Formulas.Path(_arrowSpawnPoint.position, _initialVelocity, _initialAcceleration, _angle);
    public float FlightDuration => Formulas.FlightDuration(_arrowSpawnPoint.position, _initialVelocity, _initialAcceleration, _angle);
    public float FlightDistance => Formulas.FlightDistance(_arrowSpawnPoint.position, _initialVelocity, _initialAcceleration, _angle);
    public float AverageVelocity => Formulas.AverageVelocity(_arrowSpawnPoint.position, _initialVelocity, _initialAcceleration, _angle);
    public float LandingVelocity => Formulas.LandingVelocity(_arrowSpawnPoint.position, _initialVelocity, _initialAcceleration, _angle);

    private void Awake()
    {
        Instance = this;
        _canMove = false;
    }

    public void SetTime(Vector2 time)
    {
        _t = time.x;
        ResetTime();
    }

    public void SetInitialVelocity(Vector2 velocity)
    {
        _initialVelocity = velocity;
        UpdateExitData();
        ResetTime();
    }

    public void SetInitislAcceleration(Vector2 acceleration)
    {
        _initialAcceleration = acceleration;
        _initialAcceleration.y = -9.81f;
        UpdateExitData();
        ResetTime();
    }

    public void SetHeight(float height)
    {
        _height = height;
        UpdateExitData();
        ResetTime();
    }

    public void SetAngle(float angle)
    {
        _angle = angle;
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
            _arrow = null;
        }

        if (_canMove)
            _moveRoutine = StartCoroutine(MoveArrow());
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;

        if (_canMove && _moveRoutine == null)
            _moveRoutine = StartCoroutine(MoveArrow());
    }

    private void UpdateExitData()
    {
        OnTimeChanged?.Invoke(_time);
        OnPathChanged?.Invoke(Path);
        OnFlightDurationChanged?.Invoke(FlightDuration);
        OnFlightDistanceChanged?.Invoke(FlightDistance);
        OnAverageVelocityChanged?.Invoke(AverageVelocity);
        OnLandingVelocityChanged?.Invoke(LandingVelocity);
        OnAngleChanged?.Invoke(_angle);
        OnHeightChanged?.Invoke(_height);
    }

    private IEnumerator MoveArrow()
    {
        yield return new WaitUntil(() => _time > _t);
        Debug.Log("start");

        if (_arrow == null)
            _arrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity).transform;

        while (true)
        {
            _arrow.position = Position;

            if (_arrow.position.y <= 0)
                break;

            yield return null;
        }

        _moveRoutine = null;
        _arrow = null;
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
}
