using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 velocity = InputUtils.ParseToVector(value, _axisCount);
        _moveableObject.SetInitialVelocity(velocity.x);
    }
}
