using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class PathLine : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _iterations;
    [SerializeField] private float _step;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _timeOffset;
    [SerializeField] private Transform _column;
    private Vector3 _columnScale;

    private MoveableObject _movableObject;

    private void Start()
    {
        _movableObject = MoveableObject.Instance;
        _columnScale = _column.localScale;

        _movableObject.OnTimeChanged.AddListener(AddPoint);
        _movableObject.OnTimeReseted.AddListener(Restart);

        Restart();
    }

    private void Restart()
    {
        _lineRenderer.positionCount = 0;
        CreateStart();
    }

    private void AddPoint(float time)
    {
        AddPoint(time, _timeOffset);
    }

    private void AddPoint(float time, float timeOffset)
    {
        var point = Formulas.Position(_movableObject.Radius,
                                      _movableObject.InitialVelocity,
                                      _movableObject.InitialAcceleration,
                                      _movableObject.Jerk,
                                      time + timeOffset) + _positionOffset;

        _lineRenderer.positionCount += 1;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, point);

        Vector3 columnScale = _columnScale;
        if (point.y / 2 > _column.localScale.y)
        {
            columnScale.y = point.y / 2;
            Vector3 columnPosition = _column.position;
            columnPosition.y = columnScale.y;

            _column.localScale = columnScale;
            _column.position = columnPosition;
        }
    }

    private void CreateStart()
    {
        for (float i = 0; i < _timeOffset; i += Time.deltaTime)
        {
            AddPoint(i, 0);
        }
    }
}
