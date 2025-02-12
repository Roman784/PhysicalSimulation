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
        value = value.Replace('.', ',');
        string[] parts = value.Split(' ');
        float[] numbers = new float[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            if (float.TryParse(parts[i], out float number))
            {
                numbers[i] = number;
            }
        }

        if (numbers.Length == 0) return;

        Vector3 speed = Vector3.zero;

        if (numbers.Length >= 1 && _axisCount >= 1) speed.x = numbers[0];
        if (numbers.Length >= 2 && _axisCount >= 2) speed.y = numbers[1];
        if (numbers.Length >= 3 && _axisCount >= 3) speed.z = numbers[2];

        _bird.ChangeSpeed(speed);
    }
}
