using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccelerationInput : DataInput
{
    public override void ChangeValue(string value)
    {
        Vector3 acceleration = InputUtils.ParseToVector(value, _axisCount);
        _moveableObject.SetInitislAcceleration(acceleration);
    }
}
