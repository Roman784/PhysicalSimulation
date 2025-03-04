using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 c = InputUtils.ParseToVector(value, _axisCount);
        _bird.SetC(c.x);
    }
}
