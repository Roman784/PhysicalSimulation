using UnityEngine;

public class InputUtils
{
    public static Vector3 ParseToVector(string value, int axisCount)
    {
        if (value.Length == 0)
            return Vector3.zero;

        value = value.Replace('.', ',');
        string[] parts = value.Split(' ');
        float[] numbers = new float[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            if (float.TryParse(parts[i], out float number))
            {
                numbers[i] = number;
            }
        }

        Vector3 vector = Vector3.zero;

        if (numbers.Length >= 1 && axisCount >= 1) vector.x = numbers[0];
        if (numbers.Length >= 2 && axisCount >= 2) vector.y = numbers[1];
        if (numbers.Length >= 3 && axisCount >= 3) vector.z = numbers[2];

        return vector;
    }
}
