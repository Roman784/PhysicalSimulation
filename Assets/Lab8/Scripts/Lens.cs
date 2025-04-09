using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField] private RefractiveObject _refractive;

    public void ChangeScale(Vector2 scale)
    {
        transform.localScale = scale;
    }

    public void ChangePosition(float x)
    {
        var position = new Vector3(x, transform.position.y, transform.position.z);
        transform.localPosition = position;
    }

    public void ChangeRefraction(float refraction)
    {
        _refractive.Refractive = refraction;
    }
}
