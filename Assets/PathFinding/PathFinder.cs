using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int _startCoords;
    public Vector2Int StartCoords { get { return _startCoords; } }

    [SerializeField] Vector2Int _destinationCoords;
    public Vector2Int DestinationCoords { get { return _destinationCoords; } }

    Node _startNode;
    Node _destinationNode;
    Node _currentSearchNode;

    Queue<Node> _frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] _directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager _gridManager;
    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        if (_gridManager != null)
        {
            _grid = _gridManager.Grid;
            _startNode = _grid[_startCoords];
            _destinationNode = _grid[_destinationCoords];
        }
    }

    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        _gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighborCoords = _currentSearchNode._coordinates + direction;

            if (_grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(_grid[neighborCoords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!_reached.ContainsKey(neighbor._coordinates) && neighbor._isWalkable)
            {
                neighbor._connectedTo = _currentSearchNode;
                _reached.Add(neighbor._coordinates, neighbor);
                _frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
        _startNode._isWalkable = true;
        _destinationNode._isWalkable = true;

        _frontier.Clear();
        _reached.Clear();

        bool _isRunnig = true;

        _frontier.Enqueue(_startNode);
        _reached.Add(_startCoords, _startNode);

        while (_frontier.Count > 0 && _isRunnig)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode._isExplored = true;
            ExploreNeighbors();
            if (_currentSearchNode._coordinates == _destinationCoords)
            {
                _isRunnig = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = _destinationNode;

        path.Add(_destinationNode);
        currentNode._isPath = true;

        while(currentNode._connectedTo != null)
        {
            currentNode = currentNode._connectedTo;
            path.Add(currentNode);
            currentNode._isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockThePath(Vector2Int coordiantes)
    {
        if (_grid.ContainsKey(coordiantes))
        {
            bool previousState = _grid[coordiantes]._isWalkable;
            _grid[coordiantes]._isWalkable = false;

            List<Node> newPath = GetNewPath();
            _grid[coordiantes]._isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
    }
}
