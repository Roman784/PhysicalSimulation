using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccelerationInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private int _axisCount = 1;

    private Bird _bird;

    private void Start()
    {
        _bird = Bird.Instance;
        _inputField.onValueChanged.AddListener(ChangeStartPosition);
    }

    public void ChangeStartPosition(string value)
    {
        Vector3 acceleration = InputUtils.ParseToVector(value, _axisCount);
        _bird.ChangeAcceleration(acceleration);
    }
}
