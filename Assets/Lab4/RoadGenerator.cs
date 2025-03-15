using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private int _iterations;
    [SerializeField] private float _step;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _timeOffset;

    private MoveableObject _movableObject;

    private void Start()
    {
        /*_movableObject = MoveableObject.Instance;

        var points = new List<Vector3>();
        for (float i = 0; i < _iterations * _step; i += _step)
        {
            var point = Formulas.Position(_movableObject.Radius, _movableObject.InitialVelocity, _movableObject.InitialAcceleration, i) + _positionOffset;
            points.Add(point);
        }

        Generate(points);*/

        /*_spline.Spline.Clear();

        StartCoroutine(CreateStart());

        _movableObject.OnTimeChanged.AddListener(AddPoint);*/
    }

    /*private void AddPoint(float time)
    {
        var point = Formulas.Position(_movableObject.Radius, 
                                      _movableObject.InitialVelocity, 
                                      _movableObject.InitialAcceleration, time + _timeOffset) + _positionOffset;
        _spline.Spline.Add(new BezierKnot(point), TangentMode.AutoSmooth);
    }

    private IEnumerator CreateStart()
    {
        for (float i = 0; i < _timeOffset; i += Time.deltaTime)
        {
            AddPoint(i);
            yield return null;
        }
    }*/

    /*public void Generate(List<Vector3> points)
    {
        _spline.Spline.Clear();
        
        foreach (var point in points)
        {
            var knot = new BezierKnot(point);
            _spline.Spline.Add(knot, TangentMode.AutoSmooth);
        }
    }*/
}
