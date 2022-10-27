using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int StartCurrency=150;
    [SerializeField] int CurrentCurrency;
    [SerializeField] TextMeshProUGUI GoldBalance;
    public int currentCurrency { get { return CurrentCurrency; } set { CurrentCurrency = value; } }
    void Awake()
    {
        CurrentCurrency = StartCurrency;
        GoldBalance.text = "Gold :" + CurrentCurrency;
    }
    private void Update()
    {
        GoldBalance.text = "Gold :" + CurrentCurrency;
    }
    public void Deposit(int amount)
    {
        CurrentCurrency += amount;
    }



    public void Withdraw(int amount)
    {
        CurrentCurrency -= amount;
        if (CurrentCurrency < 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    { 
        ReloadScene();
    }
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
