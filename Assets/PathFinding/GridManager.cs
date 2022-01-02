using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int _gridSize;
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
