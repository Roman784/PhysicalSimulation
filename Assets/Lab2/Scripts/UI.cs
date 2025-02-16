using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _angularVelocityView;
    [SerializeField] private TMP_Text _anglePerUnitTimeView;
    [SerializeField] private TMP_Text _angleAtCurrentTimeView;
    [SerializeField] private TMP_Text _revolutionsNumberView;
    [SerializeField] private TMP_Text _pathView;
    [SerializeField] private TMP_Text _coordinatesView;
    [SerializeField] private TMP_Text _linearVelocityView;

    [Space]

    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    [Space]

    [SerializeField] private Slider _radiusSlider;
    [SerializeField] private Slider _rotationFrequencySlider;
    [SerializeField] private Slider _timeUnitSlider;

    private Planet _planet;

    private void Start()
    {
        _planet = Planet.Instance;

        _pauseButton.onClick.AddListener(PauseSimulation);
        _resumeButton.onClick.AddListener(ResumeSimulation);

        _radiusSlider.onValueChanged.AddListener(ChangeRadius);
        _rotationFrequencySlider.onValueChanged.AddListener(ChangeRotationFrequency);
        _timeUnitSlider.onValueChanged.AddListener(ChangeTimeUnit);


        PauseSimulation();
    }

    private void Update()
    {
        UpdateView();
    }

    public void ResumeSimulation()
    {
        _planet.SetCanMove(true);

        _resumeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void PauseSimulation()
    {
        _planet.SetCanMove(false);

        _resumeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }

    public void ResetTime() => _planet.ResetTime();

    public void ChangeRadius(float value) => _planet.ChangeRadius(_radiusSlider.value);
    public void ChangeRotationFrequency(float value) => _planet.ChangeRotationFrequency(_rotationFrequencySlider.value);
    public void ChangeTimeUnit(float value) => _planet.ChangeTimeUnit(_timeUnitSlider.value);

    private void UpdateView()
    {
        _angularVelocityView.text = $"{_planet.AngularVelocity:F2}";
        _anglePerUnitTimeView.text = $"{_planet.AnglePerUnitTime * Mathf.Rad2Deg:F1}";
        _angleAtCurrentTimeView.text = $"{_planet.AngleAtCurrentTime * Mathf.Rad2Deg:F2}";
        _revolutionsNumberView.text = $"{_planet.RevolutionsNumber:F2}";
        _pathView.text = $"{_planet.Path:F2}";
        _coordinatesView.text = $"{_planet.Coordinates:F1}";
        _linearVelocityView.text = $"{_planet.LinearVelocity:F2}";
    }
}
