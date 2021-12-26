using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool _isPlaceble;
    private void OnMouseDown()
    {
        if (_isPlaceble)
        {
            Debug.Log(transform.name);
        }
    }
}
