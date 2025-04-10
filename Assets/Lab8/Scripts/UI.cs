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
    [SerializeField] private Scrollbar _positionInput;
    [SerializeField] private Scrollbar _scaleXInput;
    [SerializeField] private Scrollbar _scaleYInput;

    [Space]

    [SerializeField] private LaserPointer _laserPointer;
    [SerializeField] private EdgesCreator _edgesCreator;
    [SerializeField] private Lens _lens;
    [SerializeField] private FieldCreator _fieldCreator;

    [Space]

    [SerializeField] private string _nextSceneName;

    private void Start()
    {
        ChangeAngle();
    }

    public void ChangeAngle()
    {
        var angle = Mathf.Lerp(-270, 270, _angleInput.value);
        _laserPointer.ChangeAngle(angle * Mathf.Deg2Rad);
    }

    public void ChangeRefractions()
    {
        var refractions = ParseToList(_refractionsInput.text);
        _edgesCreator?.SetRefraction(refractions);
        if (refractions.Count > 0)
            _lens?.ChangeRefraction(refractions[0]);
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

    public void ChangeLensPosition()
    {
        var x = _positionInput.value * 10 - 5f;
        _lens.ChangePosition(x);
        ChangeAngle();
    }

    public void ChangeLensScale()
    {
        var x = _scaleXInput.value * 2;
        var y = _scaleYInput.value * 2;

        _lens.ChangeScale(new Vector2(x, y));
        ChangeAngle();
    }

    public void CreateField()
    {
        _fieldCreator.Create();
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
