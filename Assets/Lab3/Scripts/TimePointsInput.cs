using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePointsInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 points = InputUtils.ParseToVector(value, _axisCount);
        _bird.SetTimePoints(points);
    }
}
