using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _blockedColor = Color.gray;

    TextMeshPro _label;
    Vector2 _coordinates = new Vector2();
    Waypoint _waypoint;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _waypoint = GetComponentInParent<Waypoint>();
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
        if (_waypoint.IsPlaceble)
        {
            _label.color = _defaultColor;
        }
        else
        {
            _label.color = _blockedColor;
        }
    }

    private void UpdateGameojectName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    private void DisplayCoordinates()
    {
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        _label.text = _coordinates.x + "," + _coordinates.y;
    }
}
