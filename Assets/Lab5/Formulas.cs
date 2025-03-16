using UnityEngine;

public class Formulas : MonoBehaviour
{
    public static Vector2 Velocity(Vector2 initialVelocity, float angle)
    {
        float x = initialVelocity.x * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = initialVelocity.x * Mathf.Sin(angle * Mathf.Deg2Rad); // Здесь x, чтобы значение скорости было одинаково для обоих осей.

        return new Vector2(x, y);
    }

    public static Vector2 Velocity(Vector2 initialVelocity, Vector2 initialAcceleration, float angle, float time)
    {
        return Velocity(initialVelocity, angle) + Acceleration(initialAcceleration, angle) * time;
    }

    public static float AverageVelocity(Vector2 initialPosition, Vector2 initialVelocity, Vector2 initialAcceleration, float angle)
    {
        float duration = FlightDuration(initialPosition, initialVelocity, initialAcceleration, angle);
        float path = Path(initialPosition, initialVelocity, initialAcceleration, angle);

        return path / duration;
    }

    public static float LandingVelocity(Vector2 initialPosition, Vector2 initialVelocity, Vector2 initialAcceleration, float angle)
    {
        float duration = FlightDuration(initialPosition, initialVelocity, initialAcceleration, angle);
        Vector2 velocity = Velocity(initialVelocity, initialAcceleration, angle, duration);

        return Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2));
    }

    public static Vector2 Acceleration(Vector2 initialAcceleration, float angle)
    {
        float x = initialAcceleration.x * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = initialAcceleration.y;

        return new Vector2(x, y);
    }

    public static Vector2 Position(Vector2 initialPosition, Vector2 initialVelocity, Vector2 initialAcceleration, float angle, float time) => 
        initialPosition + Velocity(initialVelocity, angle) * time + Acceleration(initialAcceleration, angle) * Mathf.Pow(time, 2) / 2;

    public static float Path(Vector2 initialPosition, Vector2 initialVelocity, Vector2 initialAcceleration, float angle)
    {
        float duration = FlightDuration(initialPosition, initialVelocity, initialAcceleration, angle);
        Vector2 velocity = Velocity(initialVelocity, angle);
        Vector2 currentPosition = initialPosition;
        float timeStep = 0.02f;

        for (float t = 0; t < duration; t += timeStep)
        {
            currentPosition += velocity * timeStep + initialAcceleration * Mathf.Pow(timeStep, 2) / 2;
            velocity += initialAcceleration * timeStep;
        }

        return Vector2.Distance(initialPosition, currentPosition);
    }

    public static float FlightDuration(Vector2 initialPosition, Vector2 initialVelocity, Vector2 initialAcceleration, float angle)
    {
        float velocity = Velocity(initialVelocity, angle).y;
        float acceleration = -initialAcceleration.y;

        return (velocity + Mathf.Sqrt(Mathf.Pow(velocity, 2) + 2 * acceleration * initialPosition.y)) / acceleration;
    }

    public static float FlightDistance(Vector2 initialPosition, Vector2 initialVelocity, Vector2 initialAcceleration, float angle)
    {
        float duration = FlightDuration(initialPosition, initialVelocity, initialAcceleration, angle);
        return Position(initialPosition, initialVelocity, initialAcceleration, angle, duration).x - initialPosition.x;
    }

    private static Vector3 Pow(Vector3 value, float power) =>
            new Vector3(Mathf.Pow(value.x, power), Mathf.Pow(value.y, power));
}
