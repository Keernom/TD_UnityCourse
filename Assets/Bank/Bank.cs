using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int _startBalance = 150;
    [SerializeField] int _currentBalance;
    public int CurrentBalace { get {return _currentBalance;} }


    private void Awake()
    {
        _currentBalance = _startBalance;
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
    }

    public void WithDraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);

        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    private static void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
