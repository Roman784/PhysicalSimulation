using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Scrollbar _angleInput;
    [SerializeField] private TMP_InputField _refractionsInput;
    [SerializeField] private TMP_InputField _envCountInput;

    [Space]

    [SerializeField] private LaserPointer _laserPointer;
    [SerializeField] private EdgesCreator _edgesCreator;

    [Space]

    [SerializeField] private string _nextSceneName;

    public void ChangeAngle()
    {
        var angle = Mathf.Lerp(-180, 0, _angleInput.value);
        _laserPointer.ChangeAngle(angle * Mathf.Deg2Rad);
    }

    public void ChangeRefractions()
    {
        var refractions = ParseToList(_refractionsInput.text);
        _edgesCreator.SetRefraction(refractions);
        ChangeAngle();
    }

    public void ChnageEnvCount()
    {
        var counts = ParseToList(_envCountInput.text);
        int count = counts.Count > 0 ? (int)counts[0] : 0;
        _edgesCreator.SetCount(count);
        ChangeRefractions();
        ChangeAngle();
    }

    public void OpenNextScene()
    {
        SceneManager.LoadScene(_nextSceneName);
    }

    private List<float> ParseToList(string value)
    {
        if (value.Length == 0)
            return new List<float>();

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

        return new List<float>(numbers);
    }
}
