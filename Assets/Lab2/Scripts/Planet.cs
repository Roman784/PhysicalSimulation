using UnityEngine;

public class Planet : MonoBehaviour
{
    public static Planet Instance;

    [SerializeField] private Transform _target;
    [SerializeField] private float _radius;
    [SerializeField] private float _rotationFrequency;
    [SerializeField] private float _timeUnit;

    private float _time = 0f;
    private bool _canMove = false;

    private void Awake()
    {
        Instance = this;
    }

    public float AngularVelocity => 2 * Mathf.PI * _rotationFrequency;
    public float AnglePerUnitTime => AngularVelocity * _timeUnit;
    public float AngleAtCurrentTime => AngularVelocity * _time;
    public float RevolutionsNumber => _rotationFrequency * _timeUnit;
    public float Path => RevolutionsNumber * 2 * Mathf.PI * _radius;
    public Vector3 Coordinates => GetCoordinates(AngleAtCurrentTime);
    public float LinearVelocity => AngularVelocity * _radius;

    private void Update()
    {
        Rotate();
        IncreaseTime();
    }

    public void ChangeRadius(float newRadius) => _radius = newRadius;
    public void ChangeRotationFrequency(float newRotationFrequency) => _rotationFrequency = newRotationFrequency;
    public void ChangeTimeUnit(float newTimeUnit) => _timeUnit = newTimeUnit;
    public void ResetTime() => _time = 0f;
    public void SetCanMove(bool canMove) => _canMove = canMove;

    private void Rotate()
    {
        if (!_canMove) return;

        Vector3 position = Coordinates + _target.position;
        transform.position = position;

        // Вращение вокруг своей оси.
        transform.Rotate(Vector3.forward, _rotationFrequency * 360 * Time.deltaTime);
    }

    private Vector3 GetCoordinates(float angle)
    {
        float x = _radius * Mathf.Cos(angle);
        float z = _radius * Mathf.Sin(angle);

        return new Vector3(x, 0, z);
    }
    private void IncreaseTime()
    {
        if (!_canMove) return;
        _time += Time.deltaTime;
    }
}
