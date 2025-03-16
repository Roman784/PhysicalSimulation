using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class DataInput : MonoBehaviour
{
    [SerializeField] protected TMP_InputField _inputField;
    [SerializeField] protected int _axisCount = 1;

    protected MoveableObject _moveableObject;

    private void Start()
    {
        _moveableObject = MoveableObject.Instance;
        _inputField.onValueChanged.AddListener((value) => ChangeValue(value));
    }

    public abstract void ChangeValue(string value);
}
