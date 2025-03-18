using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] private Ball _ball1;
    [SerializeField] private Ball _ball2;

    private void Update()
    {
        float distance = Vector3.Distance(_ball1.Position, _ball2.Position);
        float radius1 = _ball1.transform.localScale.x / 2;
        float radius2 = _ball2.transform.localScale.x / 2;

        if (distance <= radius1 + radius2)
        {
            HandleCollision(_ball1, _ball2);
        }
    }

    private void HandleCollision(Ball ball1, Ball ball2)
    {
        Vector2 normal = ((Vector2)ball1.transform.position - (Vector2)ball2.transform.position).normalized;

        Vector2 velocity1Normal = Vector2.Dot(ball2.Velocity, normal) * normal;
        Vector2 velocity1Tangent = ball2.Velocity - velocity1Normal;

        Vector2 velocity2Normal = Vector2.Dot(ball1.Velocity, normal) * normal;
        Vector2 velocity2Tangent = ball1.Velocity - velocity2Normal;

        Vector2 newVelocity1Normal = (velocity1Normal * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * velocity2Normal) / (ball2.Mass + ball1.Mass);
        Vector2 newVelocity2Normal = (2 * ball2.Mass * velocity1Normal + velocity2Normal * (ball1.Mass - ball2.Mass)) / (ball2.Mass + ball1.Mass);

        Vector2 newVelocity1 = newVelocity1Normal + velocity1Tangent;
        Vector2 newVelocity2 = newVelocity2Normal + velocity2Tangent;

        ball2.SetVelocity(newVelocity1);
        ball1.SetVelocity(newVelocity2);

        _ball1.InvokeOnCollided();
        _ball2.InvokeOnCollided();
    }
}
