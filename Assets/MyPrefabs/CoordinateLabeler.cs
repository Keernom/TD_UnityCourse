﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro _label;
    Vector2 _coordinates = new Vector2();

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
            _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

            _label.text = _coordinates.x + "," + _coordinates.y;
        }
    }
}
