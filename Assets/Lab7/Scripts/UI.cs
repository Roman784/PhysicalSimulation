
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] private Scrollbar _angle;
    [SerializeField] private TMP_InputField _mu;
    [SerializeField] private TMP_InputField _mass;
    [SerializeField] private TMP_InputField _force;
    [SerializeField] private TMP_InputField _a;

    [Space]

    [SerializeField] private TMP_Text _timeView;
    [SerializeField] private TMP_Text _forceView;
    [SerializeField] private TMP_Text _frictionView;
    [SerializeField] private TMP_Text _aView;
    [SerializeField] private TMP_Text _velocityView;

    [Space]

    [SerializeField] private Transform _platformRoot;
    [SerializeField] private Platform _platform1;
    [SerializeField] private Platform _platform2;

    [Space]

    [SerializeField] private Transform _blockPos1;
    [SerializeField] private Transform _blockPos2;

    [Space]

    [SerializeField] private Block _block;

    private void Start()
    {
        ChangeValue();
        SetPos1();
    }

    private void FixedUpdate()
    {
        _timeView.text = $"{_block.Time_:F0}";
        _forceView.text = $"{_block.Force:F1}";
        _aView.text = $"{_block.A:F1}";
        _frictionView.text = $"{_block.Friction:F1}";
        _velocityView.text = $"{Mathf.Sqrt(Mathf.Pow(_block.Velocity.x, 2) + Mathf.Pow(_block.Velocity.y, 2)):F1}";
    }

    public void ChangeValue()
    {
        var angle = Mathf.Lerp(0, 60, _angle.value);

        _block.Angle = angle;
        _block.Mass = InputUtils.ParseToVector(_mass.text, 1).x;
        _block.InitialForce = InputUtils.ParseToVector(_force.text, 1).x;
        _block.Acceleration = InputUtils.ParseToVector(_a.text, 1).x;

        _platformRoot.rotation = Quaternion.Euler(0, 0, angle);
        _platform1.Mu = InputUtils.ParseToVector(_mu.text, 2).x;
        _platform2.Mu = InputUtils.ParseToVector(_mu.text, 2).y;

        _block.transform.rotation = Quaternion.Euler(0, 0, angle);
        _block.transform.position = _blockPos1.position;

        _block.SetCanMove(false);
    }

    public void StopCon()
    {
        _block.SetCanMove(!_block.CanMove);
    }

    public void SetPos1()
    {
        _block.transform.position = _blockPos1.position;
        _block.Restart();
    }

    public void SetPos2()
    {
        _block.transform.position = _blockPos2.position;
        _block.Restart();
    }
}
