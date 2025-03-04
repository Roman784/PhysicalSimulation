using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 b = InputUtils.ParseToVector(value, _axisCount);
        _bird.SetB(b.x);
    }
}
