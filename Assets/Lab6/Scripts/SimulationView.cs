using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationView : MonoBehaviour
{
    public static SimulationView Instance;

    [SerializeField] private TMP_InputField _velocityInput1;
    [SerializeField] private TMP_InputField _velocityInput2;

    [SerializeField] private TMP_Text _velocityView1;
    [SerializeField] private TMP_Text _velocityView2;

    [Space]

    [SerializeField] private Ball _ball1;
    [SerializeField] private Ball _ball2;

    private float _mass1;
    private float _mass2;

    private void Awake()
    {
        if (Instance != null)
        {
            Instance.SetBalls(_ball1, _ball2);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetBalls(Ball ball1, Ball ball2)
    {
        if (ball1 == null || ball2 == null) return;

        _ball1 = ball1;
        _ball2 = ball2;

        var velocity1 = InputUtils.ParseToVector(_velocityInput1.text, 2);
        var velocity2 = InputUtils.ParseToVector(_velocityInput2.text, 2);

        _ball1.SetMass(_mass1);
        _ball1.SetVelocity(velocity1);

        _ball2.SetMass(_mass2);
        _ball2.SetVelocity(velocity2);

        _velocityView1.text = "0";
        _velocityView2.text = "0";

        _ball1.OnCollided += (velocity) => _velocityView1.text = velocity.ToString();
        _ball2.OnCollided += (velocity) => _velocityView2.text = velocity.ToString();
    }

    public void SetTask11()
    {
        _mass1 = 1;
        _mass2 = 1;

        SceneManager.LoadScene("Lab6Task1");
    }

    public void SetTask12()
    {
        _mass1 = 1;
        _mass2 = 1000;

        SceneManager.LoadScene("Lab6Task1");
    }

    public void SetTask13()
    {
        _mass1 = 1000;
        _mass2 = 1;

        SceneManager.LoadScene("Lab6Task1");
    }

    public void SetTask21()
    {
        _mass1 = 1;
        _mass2 = 1;

        SceneManager.LoadScene("Lab6Task2");
    }
}
