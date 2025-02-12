using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _previousPosition;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float step = Vector3.Distance(_previousPosition, transform.position);
        float speed = step * 20;

        _animator.speed = Mathf.Clamp(speed, 0, 3f);
        _spriteRenderer.flipX = transform.position.x - _previousPosition.x >= 0;

        _previousPosition = transform.position;
    }
}
