using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int _gridSize;

    [SerializeField] int _unityGridSize = 10;
    public int UnityGridSize { get { return _unityGridSize; } }
    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return _grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int _coordinates)
    {
        if (_grid.ContainsKey(_coordinates))
        {
            return _grid[_coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int _coordinates)
    {
        if (_grid.ContainsKey(_coordinates))
        {
            _grid[_coordinates]._isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in _grid)
        {
            entry.Value._connectedTo = null;
            entry.Value._isExplored = false;
            entry.Value._isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / _unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / _unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * _unityGridSize;
        position.z = coordinates.y * _unityGridSize;

        return position;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int _coordinates = new Vector2Int(x, y);
                _grid.Add(_coordinates, new Node(_coordinates, true));
            }
        }
    }
}
