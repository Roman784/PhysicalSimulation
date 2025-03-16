using TMPro;
using UnityEngine;

public class ExitDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeView;
    [SerializeField] private TMP_Text _pathView;
    [SerializeField] private TMP_Text _flightDurationView;
    [SerializeField] private TMP_Text _flightDistanceView;
    [SerializeField] private TMP_Text _averageVelocityView;
    [SerializeField] private TMP_Text _landingVelocityView;

    private void Start()
    {
        var moveableObject = MoveableObject.Instance;

        moveableObject.OnTimeChanged.AddListener(UpdateTimeView);
        moveableObject.OnPathChanged.AddListener(UpdatePathView);
        moveableObject.OnFlightDurationChanged.AddListener(UpdateFlightDurationView);
        moveableObject.OnFlightDistanceChanged.AddListener(UpdateFlightDistanceView);
        moveableObject.OnAverageVelocityChanged.AddListener(UpdateAverageVelocityView);
        moveableObject.OnLandingVelocityChanged.AddListener(UpdateLandingVelocityView);
    }

    private void UpdateTimeView(float time) => _timeView.text = time.ToString();
    private void UpdatePathView(float path) => _pathView.text = path.ToString();
    private void UpdateFlightDurationView(float duration) => _flightDurationView.text = duration.ToString();
    private void UpdateFlightDistanceView(float distance) => _flightDistanceView.text = distance.ToString();
    private void UpdateAverageVelocityView(float velocity) => _averageVelocityView.text = velocity.ToString();
    private void UpdateLandingVelocityView(float landingVelocity) => _landingVelocityView.text = landingVelocity.ToString();
}
