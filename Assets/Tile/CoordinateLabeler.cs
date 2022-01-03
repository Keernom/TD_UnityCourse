using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _blockedColor = Color.gray;
    [SerializeField] Color _exploredColor = Color.yellow;
    [SerializeField] Color _pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro _label;
    Vector2Int _coordinates = new Vector2Int();
    GridManager _gridManager;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateGameojectName();
        }

        ColorChanging();
        LabelSwitcher();
    }

    private void LabelSwitcher()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.IsActive();
        }
    }

    private void ColorChanging()
    {
        if (_gridManager == null) { return; }

        Node _node = _gridManager.GetNode(_coordinates);

        if (_node == null) { return; }

        if(!_node._isWalkable)
        {
            _label.color = _blockedColor;

        }
        else if (_node._isPath)
        {
            _label.color = _pathColor;
        }
        else if (_node._isExplored)
        {
            _label.color = _exploredColor;
        }
        else
        {
            _label.color = _defaultColor;
        }
    }

    private void UpdateGameojectName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    private void DisplayCoordinates()
    {
        if (_gridManager == null) { return; }
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / _gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / _gridManager.UnityGridSize);

        _label.text = _coordinates.x + "," + _coordinates.y;
    }
}
