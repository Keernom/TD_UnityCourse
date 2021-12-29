using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHP = 5;
    [SerializeField] int _currentHP;

    Enemy _enemy;

    private void OnEnable()
    {
        _currentHP = _maxHP;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();    
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHP--;
        if (_currentHP <= 0)
        {
            gameObject.SetActive(false);
            _enemy.RewardGold();
        }
    }
}
