using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int _startBalance = 150;
    [SerializeField] int _currentBalance;
    public int CurrentBalace { get {return _currentBalance;} }

    [SerializeField] TextMeshProUGUI _displayBalance;

    private void Awake()
    {
        _currentBalance = _startBalance;
        DisplayUpdate();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        DisplayUpdate();
    }

    public void WithDraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);
        DisplayUpdate();

        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }


    void DisplayUpdate()
    {
        _displayBalance.text = "Gold: " + _currentBalance;
    }

    private static void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
