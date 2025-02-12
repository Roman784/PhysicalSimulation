using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private int _axisCount = 1;

    private Bird _bird;

    private void Start()
    {
        _bird = Bird.Instance;
        _inputField.onValueChanged.AddListener(ChangeSpeed);
    }

    public void ChangeSpeed(string value)
    {
        Vector3 speed = InputUtils.ParseToVector(value, _axisCount);
        _bird.ChangeSpeed(speed);
    }
}
