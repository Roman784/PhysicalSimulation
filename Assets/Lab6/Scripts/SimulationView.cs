using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationView : MonoBehaviour
{
    public static SimulationView Instance;

    [SerializeField] private TMP_InputField _velocityInput;
    [SerializeField] private Scrollbar _angleInput;

    [SerializeField] private TMP_Text _velocityView;

    [Space]

    [SerializeField] private Ball _ball;

    [Space]

    [SerializeField] private GameObject _stopButton;
    [SerializeField] private GameObject _continueButton;

    [Space]

    [SerializeField] private ObstaclesGenerator _obstaclesGenerator;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _ball.OnVelocityChanged += (velocity) =>
        {
            _velocityView.text = $"{velocity} | {(Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y))}";
        };

        Stop();
    }

    public void Launch()
    {
        var angle = _angleInput.value * 360 * Mathf.Deg2Rad;
        var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        var velocity = InputUtils.ParseToVector(_velocityInput.text, 1).x * direction;

        _ball.ResetPosition();
        _ball.SetVelocity(velocity);
    }

    public void RotateBallArrow()
    {
        var angle = _angleInput.value * 360;
        _ball.RotateArrow(angle);
    }

    public void Stop()
    {
        _ball.SetCanMove(false);
        _stopButton.SetActive(false);
        _continueButton.SetActive(true);
    }

    public void Continue()
    {
        _ball.SetCanMove(true);
        _stopButton.SetActive(true);
        _continueButton.SetActive(false);
    }

    public void CreateField()
    {
        _obstaclesGenerator.Create();
    }
}
