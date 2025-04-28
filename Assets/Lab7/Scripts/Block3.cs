
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Block3 : Block
{
    [SerializeField] private Transform _horizontalPoint;

    [Space]

    [SerializeField] private Block1 _block1;

    private void Update()
    {
        transform.position = new Vector3 (_horizontalPoint.position.x, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, Angle);
    }
    protected override void Move()
    {
        float velocity = Mathf.Sqrt(Mathf.Pow(_block1.Velocity.x, 2) + Mathf.Pow(_block1.Velocity.y, 2));

        if (!_canMove || Mass <= 0) return;
        
        transform.position += Vector3.down * velocity * Mathf.Sign(_block1.Force) * Time.deltaTime;
    }
}
