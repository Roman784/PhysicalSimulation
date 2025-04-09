using System.Collections;
using UnityEngine;

public class Edge : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _view;
    [SerializeField] private EdgeCollider2D _collider;
    [SerializeField] private RefractiveObject _refractive;
    [SerializeField] private RefractiveObject _refractiveAir;

    private void Awake()
    {
        _refractive.Refractive = 1f;
        ChangeColor();
    }

    public void ChangeColor()
    {
        var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 0.5f);
        _spriteRenderer.color = color;
    }

    public void SetSize(float y)
    {
        _view.localScale = new Vector2(_view.localScale.x, y);
    }

    public void SetPosition(float y)
    {
        transform.position = new Vector3(0, y, transform.position.z);
    }

    public void SetEnabledCollider(bool value)
    {
        _collider.enabled = value;
    }

    public void SetRefractive(float value)
    {
        _refractive.Refractive = value;
    }

    public void RemoveAir()
    {
        _refractiveAir.Refractive = _refractive.Refractive;
    }

    public void SetAir()
    {
        _refractiveAir.Refractive = 1;
    }
}
