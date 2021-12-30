using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Node _node;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_node._coordiantes);
        Debug.Log(_node._isWalkable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
