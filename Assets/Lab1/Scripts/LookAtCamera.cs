using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Rotate(_camera.transform);
    }

    private void Rotate(Transform target)
    {
        Vector3 rotation = target.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
    }

    [ContextMenu("Rotate")]
    private void Rotate()
    {
        Camera camera = Camera.main;
        Rotate(camera.transform);
    }
}
