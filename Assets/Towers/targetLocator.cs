using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLocator : MonoBehaviour
{
    [SerializeField] float _range = 15f;
    Transform _target;
    [SerializeField] ParticleSystem _particleBolts;
    [SerializeField] Transform _weapon;

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _target = closestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, _target.transform.position);

        if (targetDistance > _range)
        {
            Attack(false);
        }
        else
        {
            Attack(true);
        }

        _weapon.LookAt(_target);
    }

    void Attack(bool isActive)
    {
        var emissionModule = _particleBolts.emission;
        emissionModule.enabled = isActive;
    }
}
