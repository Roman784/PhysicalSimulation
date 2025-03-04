using Lab3;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class DataInput : MonoBehaviour
{
    [SerializeField] protected TMP_InputField _inputField;
    [SerializeField] protected int _axisCount = 1;

    protected BirdLab3 _bird;

    private void Start()
    {
        _bird = BirdLab3.Instance;
        _inputField.onValueChanged.AddListener((value) => ChangeValue(value));
    }

    public abstract void ChangeValue(string value);
}
