using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RadiusInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 radius = InputUtils.ParseToVector(value, _axisCount);
        _moveableObject.SetRadius(radius.x);
    }
}
