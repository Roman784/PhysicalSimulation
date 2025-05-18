
using System.Collections;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] private Piston _piston1;
    [SerializeField] private Piston _piston2;

    [Space]

    [SerializeField] private Vector2 _borders;

    public float S1;
    public float S2;
    public float M;
    public float F;

    public bool CanMove = false;

    private float _g = 9.81f;

    public float F2 { get; private set; }

    public IEnumerator UpdateRoutine()
    {
        var time = 0f;

        while (CanMove)
        {
            yield return null;

            var f2 = (F * S2 / S1) - (M * _g);
            var a = f2 / M;
            var v = a * time;

            F2 = f2;

            if (_piston1.transform.position.y > _borders.x && _piston1.transform.position.y < _borders.y &&
                _piston2.transform.position.y > _borders.x && _piston1.transform.position.y < _borders.y)
            {
                _piston1.transform.Translate(-Vector2.up * v * Time.deltaTime);
                _piston2.transform.Translate(Vector2.up * v * Time.deltaTime);
            }

            time += Time.deltaTime;
        }
    }

    public void Reset_()
    {
        _piston1.Reset_();
        _piston2.Reset_();
    }
}