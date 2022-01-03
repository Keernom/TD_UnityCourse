﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower _towerBallista;

    [SerializeField] bool _isPlaceble;
    public bool IsPlaceble { get { return _isPlaceble; } }

    GridManager _gridManager;
    PathFinder _pathFinder;
    Vector2Int _coordinates = new Vector2Int();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);

            if(!_isPlaceble)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (_gridManager.GetNode(_coordinates)._isWalkable && !_pathFinder.WillBlockThePath(_coordinates))
        {
            bool _isPlaced = _towerBallista.CreateTower(_towerBallista, transform.position);
            _isPlaceble = !_isPlaced;
            _gridManager.BlockNode(_coordinates);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Nature")
        {
            _isPlaceble = false;
        }
    }
}
