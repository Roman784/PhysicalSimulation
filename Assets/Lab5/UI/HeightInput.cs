using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 height = InputUtils.ParseToVector(value, _axisCount);
        _moveableObject.SetHeight(height.x);
    }
}
