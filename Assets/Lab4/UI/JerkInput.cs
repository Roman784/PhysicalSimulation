using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JerkInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 jerk = InputUtils.ParseToVector(value, _axisCount);
        _moveableObject.SetJerk(jerk.x);
    }
}
