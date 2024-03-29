﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _goldReward = 25;
    [SerializeField] int _goldPenalty = 25;
    
    Bank _bank;

    private void Awake()
    {
        _bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (_bank == null) { return; }
        _bank.Deposit(_goldReward);
    }

    public void PenaltyGold()
    {
        if (_bank == null) { return; }
        _bank.WithDraw(_goldPenalty);
    }
}
