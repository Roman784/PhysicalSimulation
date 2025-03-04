using Lab3;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    private BirdLab3 _bird;

    private void Start()
    {
        _bird = BirdLab3.Instance;

        _pauseButton.onClick.AddListener(PauseSimulation);
        _resumeButton.onClick.AddListener(ResumeSimulation);

        PauseSimulation();
    }

    public void ResumeSimulation()
    {
        _bird.SetCanMove(true);

        _resumeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void PauseSimulation()
    {
        _bird.SetCanMove(false);

        _resumeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }

    public void ResetTime() => _bird.ResetTime();
}
