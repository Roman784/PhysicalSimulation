using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _layouts;

    private GameObject _currentLayout;

    public void Create()
    {
        if (_currentLayout != null)
            DestroyImmediate(_currentLayout);

        _currentLayout = Instantiate(_layouts[Random.Range(0, _layouts.Length)]);

        Lens[] allLens = FindObjectsOfType<Lens>();
        foreach (var lens in allLens)
        {
            lens.ChangeRefraction(Random.Range(1f, 3f));
        }
    }
}
