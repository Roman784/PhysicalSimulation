using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 a = InputUtils.ParseToVector(value, _axisCount);
        _bird.SetA(a.x);
    }
}
