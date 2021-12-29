using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int _cost = 75;

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalace >= _cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.WithDraw(_cost);
            return true;
        }

        return false;
    }
}
