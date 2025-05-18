
using UnityEngine;

public class Piston : MonoBehaviour
{
    private Vector2 _initialPos;

    private void Awake()
    {
        _initialPos = transform.position;
    }

    public void Reset_()
    {
        transform.position = _initialPos;
    }
}
