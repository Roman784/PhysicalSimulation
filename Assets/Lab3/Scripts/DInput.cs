using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 d = InputUtils.ParseToVector(value, _axisCount);
        _bird.SetD(d.x);
    }
}
