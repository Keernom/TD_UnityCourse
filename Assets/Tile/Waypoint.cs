using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower _towerBallista;

    [SerializeField] bool _isPlaceble;
    public bool IsPlaceble { get { return _isPlaceble; } }

    private void OnMouseDown()
    {
        if (_isPlaceble)
        {
            bool _isPlaced = _towerBallista.CreateTower(_towerBallista, transform.position);
            _isPlaceble = !_isPlaced;
        }
    }
}
