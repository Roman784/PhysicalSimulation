using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Formulas : MonoBehaviour
{
    public static float Velocity(float initialVelocity, float initialAcceleration, float jerk, float time)
    {
        return initialVelocity + initialAcceleration * time + (jerk * Mathf.Pow(time, 2)) / 2;
    }

    public static float Acceleration(float initialVelocity, float jerk, float time)
    {
        return initialVelocity + jerk * time;
    }

    public static Vector3 Position(float radius, float initialVelocity, float initialAcceleration, float jerk, float time)
    {
        float x = radius * Mathf.Cos(initialVelocity * time);
        float z = radius * Mathf.Sin(initialVelocity * time);
        float y = initialVelocity * time + initialAcceleration * Mathf.Pow(time, 2) / 2 + jerk * Mathf.Pow(time, 3) / 6;

        return new Vector3(x, y, z);
    }

    public static float Path(float initialVelocity, float initialAcceleration, float jerk, float radius, float time)
    {
        float path_z = initialVelocity * time + (initialAcceleration * Mathf.Pow(time, 2)) / 2 + (jerk * Mathf.Pow(time, 3)) / 6;
        float path_perp = radius * initialVelocity * time;

        return Mathf.Sqrt(Mathf.Pow(path_z, 2) + Mathf.Pow(path_perp, 2));
    }

    private static Vector3 Pow(Vector3 value, float power) =>
            new Vector3(Mathf.Pow(value.x, power), Mathf.Pow(value.y, power));
}
