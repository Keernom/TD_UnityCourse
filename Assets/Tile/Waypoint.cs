using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject _towerBallista;

    [SerializeField] bool _isPlaceble;
    public bool IsPlaceble { get { return _isPlaceble; } }

    private void OnMouseDown()
    {
        if (_isPlaceble)
        {
            Instantiate(_towerBallista, transform.position, Quaternion.identity);
            _isPlaceble = false;
        }
    }
}
