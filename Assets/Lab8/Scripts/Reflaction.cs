using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflaction : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    public void ThrowRay(Vector2 startPoint, Vector2 endPoint)
    {
        _lineRenderer.positionCount = 2;

        _lineRenderer.SetPosition(0, startPoint);
        _lineRenderer.SetPosition(1, endPoint);
    }
}
