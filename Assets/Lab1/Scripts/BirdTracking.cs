using UnityEngine;

public class BirdTracking : MonoBehaviour
{
    [SerializeField] private bool _freezeY;

    [SerializeField] private Transform _bird;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _bird.position;
    }

    private void LateUpdate()
    {
        Vector3 position = _bird.position + _offset;

        if (_freezeY)
            position.y = transform.position.y;

        transform.position = _bird.position + _offset;
    }
}
