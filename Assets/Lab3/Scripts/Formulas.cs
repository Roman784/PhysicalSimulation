using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Lab3
{
    public class Formulas
    {
        public static Vector2 Acceleration(Vector2 initialAcceleration, Vector2 jerk, float time) =>
            initialAcceleration + jerk * time;

        public static Vector2 Velocity(Vector2 initialVelocity, Vector2 initialAcceleration, Vector2 jerk, float time) => 
            initialVelocity + initialAcceleration * time + 0.5f * jerk * Pow(jerk, 2);

        public static Vector2 Position(Vector2 initialPosition, Vector2 initialVelocity, 
                                       Vector2 initialAcceleration, Vector2 jerk, float time) => 
            initialPosition + initialVelocity * time + initialAcceleration * Mathf.Pow(time, 2) / 2 + jerk * Mathf.Pow(time, 3) / 6;

        public static float Path(Vector2 initialPosition, Vector2 initialVelocity, Vector2 initialAcceleration, Vector2 jerk, float time) => 
            CalculatePath(initialPosition, Position(initialPosition, initialVelocity, initialAcceleration, jerk, time));

        public static float CalculatePath(Vector3 initialPosition, Vector3 endPosition)
        {
            float x = Mathf.Pow(endPosition.x - initialPosition.x, 2);
            float y = Mathf.Pow(endPosition.y - initialPosition.y, 2);
            float z = Mathf.Pow(endPosition.z - initialPosition.z, 2);

            return Mathf.Sqrt(x + y + z);
        }

        private static Vector2 Pow(Vector2 value, float power) => 
            new Vector2(Mathf.Pow(value.x, power), Mathf.Pow(value.y, power));
    }
}
