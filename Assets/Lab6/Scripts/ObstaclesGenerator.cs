using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Obstacle;

public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] private Obstacle[] _prefabs;
    [SerializeField] private Transform _topLeftBorderPoint;
    [SerializeField] private Transform _bottomRightBorderPoint;

    [Space]

    [SerializeField] private Transform _aPoint;

    private List<Obstacle> _createdObstacles = new();

    private void Start()
    {
        Create();
    }

    public void Create()
    {
        DestroyCreatedObstacles();

        for (int i = 0; i < 9; i++)
        {
            var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            var newObstacle = Instantiate(prefab, GetPosition(), GetRotation());
            _createdObstacles.Add(newObstacle);
        }

        for (int i = 0; i < 3; i++)
        {
            _createdObstacles[i].Init(ObstacleType.Static);
        }

        for (int i = 3; i < 6; i++)
        {
            _createdObstacles[i].Init(ObstacleType.Movable);
        }

        for (int i = 6; i < 9; i++)
        {
            _createdObstacles[i].Init(ObstacleType.Booster);
        }

        _aPoint.position = GetPosition();
    }

    private Vector2 GetPosition()
    {
        var x = Random.Range(_topLeftBorderPoint.position.x, _bottomRightBorderPoint.position.x);
        var y = Random.Range(_bottomRightBorderPoint.position.y, _topLeftBorderPoint.position.y);
        return new Vector2(x, y);
    }

    private Quaternion GetRotation()
    {
        var angle = Random.Range(0, 360);
        return Quaternion.Euler(0, 0, angle);
    }

    private void DestroyCreatedObstacles()
    {
        foreach(var obstacle in _createdObstacles)
        {
            Destroy(obstacle.gameObject);
        }

        _createdObstacles.Clear();
    }
}
