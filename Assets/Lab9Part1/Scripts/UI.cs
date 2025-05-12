using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _speedInput;
    [SerializeField] private TMP_InputField _mInput;
    [SerializeField] private TMP_InputField _dInput;
    [SerializeField] private TMP_InputField _zInput;

    [Space]

    [SerializeField] private TMP_Text _outView;

    [Space]

    [SerializeField] private List<Gear> _gears;

    [Space]

    [SerializeField] private GameObject _stopButton;
    [SerializeField] private GameObject _conButton;

    [Space]

    [SerializeField] private string _nextSceneName;

    private float _lastGearSpeed;

    private bool _isError;
    private string _errorText;

    private void Start()
    {
        ChangeValue();
    }

    public void ChangeValue()
    {
        Stop();

        var speed = ParseToList(_speedInput.text, 1)[0];
        var z = ParseToList(_zInput.text, 3);
        var d = ParseToList(_dInput.text, 3);

        var z1 = z[0];
        var z2 = z[1];
        var z3 = z[2];

        var d1 = Mathf.Clamp(d[0], 1, d[0]);
        var d2 = Mathf.Clamp(d[1], 1, d[1]);
        var d3 = Mathf.Clamp(d[2], 1, d[2]);

        var ratio1 = z2 / z1;
        var ratio2 = z3 / z2;

        var m1 = d1 / z1;
        var m2 = d2 / z2;
        var m3 = d3 / z3;

        _lastGearSpeed = speed / (ratio1 * ratio2);

        _gears[0].Speed = speed;

        _gears[0].Ratio = ratio1;
        _gears[1].Ratio = ratio2;

        _gears[0].D = d1;
        _gears[1].D = d2;
        _gears[2].D = d3;

        _gears[0].UpdateDiametr();

        _isError = true;
        if (m1 != m2 && m2 != m3)
            _errorText = "Модули не совпадают";
        else if (z1 < 10 || z2 < 10 || z3 < 10)
            _errorText = "Мало зубьев";
        else if (ratio1 < 0.1f || ratio2 < 0.1f || ratio1 > 10f || ratio2 > 10f)
            _errorText = "Передаточное число выходит за разумные пределы";
        else
            _isError = false;
    }

    public void Stop()
    {
        _gears[0].CanMove = false;

        _stopButton.SetActive(false);
        _conButton.SetActive(true);
    }

    public void Continue()
    {
        if (_isError)
        {
            _outView.text = _errorText;
            return;
        }
        else
            _outView.text = $"Скорость: {_lastGearSpeed:F2}\n" +
                            $"Частота: {(_lastGearSpeed / (2 * 3.14f)):F2} об/сек";

        _gears[0].CanMove = true;
        StartCoroutine(_gears[0].UpdateRoutine());

        _stopButton.SetActive(true);
        _conButton.SetActive(false);
    }

    public void OpenNextScene()
    {
        SceneManager.LoadScene(_nextSceneName);
    }

    private List<float> ParseToList(string value, int capasity)
    {
        var list = new List<float>();

        if (value.Length == 0)
        {
            for (int i = 0; i < capasity; i++)
                list.Add(0f);
            return list;
        }

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

        for (int i = 0; i < capasity; i++)
        {
            if (i < numbers.Length)
                list.Add(numbers[i]);
            else
                list.Add(0f);
        }

        return list;
    }
}
