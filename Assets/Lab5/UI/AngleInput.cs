using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector2 angle = InputUtils.ParseToVector(value, _axisCount);
        _moveableObject.SetAngle(angle.x);
    }
}
