using System.Collections;
using UnityEngine;

public class Simulation2 : MonoBehaviour
{
    [SerializeField] private MovableObject _object;
    [SerializeField] private Area _area;

    public float P1;
    public float P2;
    public float V;
    public float Vel;

    public bool CanMove = false;

    public Vector2 Velocity { get; private set; }

    private float _g = 9.81f;

    public IEnumerator UpdateRoutine()
    {
        var time = 0f;
        var y = 0f;
        var x = 0f;

        while (CanMove)
        {
            yield return null;

            _area.P = P2;
            x = Vel;

            var pArea = _object.AreaP;
            var m = P1 * V;
            var fg = m * _g;
            var fa = pArea * V * _g;
            var f = fa - fg;
            var fd = -y * _object.AreaK;
            var a = (f + fd) / m;

            y += a * Time.deltaTime;

            if (Mathf.Abs(y + a * Time.deltaTime) < 0.5f && 
                _object.transform.position.y < 2.25f && _object.transform.position.y > 2.15f)
                y = 0f;

            if (_object.transform.position.y + y * Time.deltaTime < -3)
                y = 0f;
            if (_object.transform.position.x + x * Time.deltaTime < -7.8f || _object.transform.position.x + x * Time.deltaTime > 5.4f)
                x = 0f;

            var velocity = new Vector2(x, y);
            Velocity = velocity;

            _object.transform.Translate(velocity * Time.deltaTime);

            time += Time.deltaTime;
        }
    }

    public void Reset_()
    {
        _object.Reset_();
    }
}
