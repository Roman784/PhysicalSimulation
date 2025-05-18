
using Unity.VisualScripting;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private Vector2 _initialPos;

    private Area _area;
    public float AreaP =>  transform.position.y < 2.2f ? _area?.P ?? 0f : 0f;
    public float AreaK => transform.position.y < 2.2f ? 1f : 0f;

    private void Awake()
    {
        _initialPos = transform.position;
    }

    public void Reset_()
    {
        transform.position = _initialPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Area")
            _area = collision.GetComponent<Area>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Area")
            _area = null;
    }
}
