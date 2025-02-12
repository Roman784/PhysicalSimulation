using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [SerializeField] private MoveType _moveType;

    [Space]

    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    private Bird _bird;

    private void Start()
    {
        _bird = Bird.Instance;

        _bird.SetMoveType(_moveType);

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
