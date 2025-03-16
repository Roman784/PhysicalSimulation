using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector2 time = InputUtils.ParseToVector(value, _axisCount);
        _moveableObject.SetTime(time);
    }
}
