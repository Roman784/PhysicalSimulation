using TMPro;
using UnityEngine;

public class ExitDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeView;
    [SerializeField] private TMP_Text _pathView;
    [SerializeField] private TMP_Text _velocityView;
    [SerializeField] private TMP_Text _accelerationView;
    [SerializeField] private TMP_Text _positionView;
    [SerializeField] private TMP_Text _t1PositionView;

    private void Start()
    {
        var moveableObject = MoveableObject.Instance;

        moveableObject.OnTimeChanged.AddListener(UpdateTimeView);
        moveableObject.OnPathChanged.AddListener(UpdatePathView);
        moveableObject.OnVelocityChanged.AddListener(UpdateVelocityView);
        moveableObject.OnAccelerationChanged.AddListener(UpdateAccelerationView);
        moveableObject.OnPositionChanged.AddListener(UpdatePositionView);
        moveableObject.OnT1PositionChanged.AddListener(UpdateT1PositionView);
    }

    private void UpdateTimeView(float time) => _timeView.text = time.ToString();
    private void UpdatePathView(float path) => _pathView.text = path.ToString();
    private void UpdateVelocityView(float coords) => _velocityView.text = coords.ToString();
    private void UpdateAccelerationView(float speed) => _accelerationView.text = speed.ToString();
    private void UpdatePositionView(Vector3 positon) => _positionView.text = positon.ToString();
    private void UpdateT1PositionView(Vector3 positon) => _t1PositionView.text = positon.ToString();
}
