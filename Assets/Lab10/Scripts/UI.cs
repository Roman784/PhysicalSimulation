using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _sInput;
    [SerializeField] private TMP_InputField _mInput;
    [SerializeField] private TMP_InputField _fInput;

    [Space]

    [SerializeField] private TMP_InputField _p1Input;
    [SerializeField] private TMP_InputField _p2Input;
    [SerializeField] private TMP_InputField _vInput;
    [SerializeField] private TMP_InputField _velInput;

    [Space]

    [SerializeField] private TMP_Text _view;

    [Space]

    [SerializeField] private Simulation _simulation;
    [SerializeField] private Simulation2 _simulation2;

    [Space]

    [SerializeField] private GameObject _stopButton;
    [SerializeField] private GameObject _conButton;

    [Space]

    [SerializeField] private string _nextSceneName;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_simulation != null)
            _view.text = $"F2: {Mathf.Abs(_simulation.F2):F2}";
        else if (_simulation2 != null)
            _view.text = $"Скорость: {_simulation2.Velocity:F2}";
    }

    public void Change()
    {
        _simulation.S1 = ParseToList(_sInput.text, 2)[0];
        _simulation.S2 = ParseToList(_sInput.text, 2)[1];
        _simulation.M = ParseToList(_mInput.text, 1)[0];
        _simulation.F = ParseToList(_fInput.text, 1)[0];

        Stop();
    }

    public void Change2()
    {
        _simulation2.P1 = ParseToList(_p1Input.text, 1)[0];
        _simulation2.P2 = ParseToList(_p2Input.text, 1)[0];
        _simulation2.V = ParseToList(_vInput.text, 1)[0];
        _simulation2.Vel = ParseToList(_velInput.text, 1)[0];
    }

    public void Stop()
    {
        if (_simulation != null)
        {
            _simulation.CanMove = false;
            _simulation.Reset_();
        }
        else if (_simulation2 != null)
        {
            _simulation2.CanMove = false;
            _simulation2.Reset_();
        }

        _stopButton.SetActive(false);
        _conButton.SetActive(true);
    }

    public void Continue()
    {
        if (_simulation != null)
        {
            _simulation.CanMove = true;
            StartCoroutine(_simulation.UpdateRoutine());
        }
        else if (_simulation2 != null)
        {
            _simulation2.CanMove = true;
            StartCoroutine(_simulation2.UpdateRoutine());
        }

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
