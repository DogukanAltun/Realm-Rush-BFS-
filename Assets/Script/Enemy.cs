using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int penalty = 25;
    private int reward = 25;
    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void GoldPenalty()
    {
        bank.Withdraw(penalty);
    }

    public void GoldReward()
    {
        bank.Deposit(reward);
    }
}
