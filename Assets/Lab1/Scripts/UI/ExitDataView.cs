using Lab3;
using TMPro;
using UnityEngine;

public class ExitDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeView;
    [SerializeField] private TMP_Text _pathView;
    [SerializeField] private TMP_Text _velocityView;
    [SerializeField] private TMP_Text _accelerationView;

    private void Start()
    {
        BirdLab3 bird = BirdLab3.Instance;

        bird.OnTimeChanged.AddListener(UpdateTimeView);
        bird.OnPathChanged.AddListener(UpdatePathView);
        bird.OnVelocityChanged.AddListener(UpdateVelocityView);
        bird.OnAccelerationChanged.AddListener(UpdateAccelerationView);
    }

    private void UpdateTimeView(float time) => _timeView.text = time.ToString();
    private void UpdatePathView(float path) => _pathView.text = path.ToString();
    private void UpdateVelocityView(Vector2 coords) => _velocityView.text = coords.ToString();
    private void UpdateAccelerationView(Vector2 speed) => _accelerationView.text = speed.ToString();
}
