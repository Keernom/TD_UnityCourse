using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLocator : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _weapon;

    void Start()
    {
        _target = FindObjectOfType<EnemyMover>().transform;
    }

    void Update()
    {
        _weapon.LookAt(_target);
    }
}
