using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class enemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHP = 5;
    [Tooltip("Adds amount to maxHP when enemy dies")]
    [SerializeField] int _difficultyRamp = 1;
    int _currentHP;

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
            _maxHP += _difficultyRamp;
            _enemy.RewardGold();
        }
    }
}
