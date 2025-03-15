using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    private MoveableObject _moveableObject;

    private void Start()
    {
        _moveableObject = MoveableObject.Instance;

        _pauseButton.onClick.AddListener(PauseSimulation);
        _resumeButton.onClick.AddListener(ResumeSimulation);

        PauseSimulation();
    }

    public void ResumeSimulation()
    {
        _moveableObject.SetCanMove(true);

        _resumeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void PauseSimulation()
    {
        _moveableObject.SetCanMove(false);

        _resumeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }

    public void ResetTime() => _moveableObject.ResetTime();
}
