using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgesCreator : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private Edge _edgePrefab;
    [SerializeField] private Edge _airEdge;

    [Space]

    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;

    [SerializeField] private List<Edge> _edges = new();

    public List<Edge> Edges => _edges;

    private float TotalHeight => _topPoint.position.y - _bottomPoint.position.y;
    private float EdgeHeight => TotalHeight / _count;

    private void Awake()
    {
        if (_count > 0)
            Create();
    }

    public void SetCount(int count)
    {
        _count = count;

        if (_count > 0)
            Create();
    }

    [ContextMenu("Create")]
    public void Create()
    {
        DestroyEdges();

        for (int i = 0; i < _count; i++)
        {
            var newEdge = Instantiate(_edgePrefab);
            _edges.Add(newEdge);

            var yPos = _topPoint.position.y - i * EdgeHeight;

            newEdge.SetPosition(yPos);
            newEdge.SetSize(EdgeHeight);
        }
    }

    public void EnableColliders()
    {
        foreach (var edge in _edges)
        {
            edge.SetEnabledCollider(true);
        }
        _airEdge.SetEnabledCollider(true);
    }

    public void SetRefraction(List<float> refractions)
    {
        for (int i = 0; i < _edges.Count; i++)
        {
            if (refractions.Count > i)
                if (refractions[i] == 0)
                    _edges[i].SetRefractive(1);
                else
                _edges[i].SetRefractive(refractions[i]);
            else
                _edges[i].SetRefractive(1);
        }
    }

    private void DestroyEdges()
    {
        for (int i = 0; i < _edges.Count; i++)
        {
            Destroy(_edges[i].gameObject);
        }
        _edges.Clear();
    }
}
