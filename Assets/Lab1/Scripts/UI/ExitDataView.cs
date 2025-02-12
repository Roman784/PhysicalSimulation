using TMPro;
using UnityEngine;

public class ExitDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeView;
    [SerializeField] private TMP_Text _pathView;
    [SerializeField] private TMP_Text _coordsView;
    [SerializeField] private TMP_Text _speedView;

    private void Start()
    {
        Bird bird = Bird.Instance;

        if (_timeView != null)
            bird.OnTimeChanged.AddListener(UpdateTimeView);

        if (_pathView != null)
            bird.OnPathChanged.AddListener(UpdatePathView);

        if (_coordsView != null)
            bird.OnPositionChanged.AddListener(UpdateCoordsView);

        if (_speedView != null)
            bird.OnSpeedChanged.AddListener(UpdateSpeedView);
    }

    private void UpdateTimeView(float time) => _timeView.text = time.ToString();
    private void UpdatePathView(float path) => _pathView.text = path.ToString();
    private void UpdateCoordsView(Vector3 coords) => _coordsView.text = coords.ToString();
    private void UpdateSpeedView(Vector3 speed) => _speedView.text = speed.ToString();
}
