using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lens : MonoBehaviour
{
    [SerializeField] private RefractiveObject _refractive;
    [SerializeField] private TMP_Text _refractiveView;

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
        _refractiveView.text = $"{refraction:F1}";
    }
}
