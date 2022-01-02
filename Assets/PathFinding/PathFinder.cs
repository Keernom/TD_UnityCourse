﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int _startCoords;
    [SerializeField] Vector2Int _destinationCoords;

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
        }

        _startNode = new Node(_startCoords, true);
        _destinationNode = new Node(_destinationCoords, true);
    }

    private void Start()
    {
        BreadthFirstSearch();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighborCoords = _currentSearchNode._coordiantes + direction;

            if (_grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(_grid[neighborCoords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (! _reached.ContainsKey(neighbor._coordiantes) && neighbor._isWalkable)
            {
                _reached.Add(neighbor._coordiantes, neighbor);
                _frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool _isRunnig = true;

        _frontier.Enqueue(_startNode);
        _reached.Add(_startCoords, _startNode);

        while (_frontier.Count > 0 && _isRunnig)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode._isExplored = true;
            ExploreNeighbors();
            if (_currentSearchNode._coordiantes == _destinationCoords)
            {
                _isRunnig = false;
            }
        }
    }
}
