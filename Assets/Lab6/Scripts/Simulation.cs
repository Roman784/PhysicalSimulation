using UnityEngine;
using static Obstacle;

public class Simulation
{
    public void HandleCollision(Ball ball, Obstacle obstacle, ContactPoint2D contact)
    {
        Vector2 normal = contact.normal;

        Vector2 velocity1Normal = Vector2.Dot(obstacle.Velocity, normal) * normal;
        Vector2 velocity1Tangent = obstacle.Velocity - velocity1Normal;

        Vector2 velocity2Normal = Vector2.Dot(ball.Velocity, normal) * normal;
        Vector2 velocity2Tangent = ball.Velocity - velocity2Normal;

        Vector2 newVelocity1Normal = (velocity1Normal * (obstacle.Mass - ball.Mass) + 2 * ball.Mass * velocity2Normal) / (obstacle.Mass + ball.Mass);
        Vector2 newVelocity2Normal = (2 * obstacle.Mass * velocity1Normal + velocity2Normal * (ball.Mass - obstacle.Mass)) / (obstacle.Mass + ball.Mass);

        Vector2 newVelocity1 = newVelocity1Normal + velocity1Tangent;
        Vector2 newVelocity2 = newVelocity2Normal + velocity2Tangent;

        if (obstacle.Type == ObstacleType.Booster)
            newVelocity2 *= 1.5f;

        obstacle.SetVelocity(newVelocity1);
        ball.SetVelocity(newVelocity2);
    }
}
