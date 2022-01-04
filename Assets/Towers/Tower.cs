using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int _cost = 75;
    [SerializeField] int _buildDelay = 1;
    bool _isTowerActive = true;
    private void Start()
    {
        StartCoroutine(Build());
    }

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

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in transform)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(_buildDelay);
            foreach (Transform grandchild in transform)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
        Debug.Log("HELLO - CORURINE");
    }
}
