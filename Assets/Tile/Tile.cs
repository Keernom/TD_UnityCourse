using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower _towerBallista;

    [SerializeField] bool _isPlaceble;
    public bool IsPlaceble { get { return _isPlaceble; } }

    GridManager _gridManager;
    Vector2Int _coordinates = new Vector2Int();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
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
        if (_isPlaceble)
        {
            bool _isPlaced = _towerBallista.CreateTower(_towerBallista, transform.position);
            _isPlaceble = !_isPlaced;
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
