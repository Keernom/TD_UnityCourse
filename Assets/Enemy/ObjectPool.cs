using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] [Range(0, 50)] int _poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float _waitTime = 1f;

    GameObject[] _pool;

    private void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    private void PopulatePool()
    {
        _pool = new GameObject[_poolSize];

        for (int i = 0; i < _pool.Length; i++)
        {
            _pool[i] = Instantiate(_enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }

    void EnableElementsInPool()
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (_pool[i].activeInHierarchy == false)
            {
                _pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            EnableElementsInPool();
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
