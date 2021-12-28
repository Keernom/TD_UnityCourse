using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHP = 5;
    [SerializeField] int _currentHP;

    private void OnEnable()
    {
        _currentHP = _maxHP;
    }

    private void OnParticleCollision(GameObject other)
    {
        _currentHP--;
        if (_currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
