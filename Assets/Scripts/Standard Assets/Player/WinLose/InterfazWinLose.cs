using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfazWinLose : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;
    
    private void LoseGame()
    {
        Vector2 positionElement = Camera.main.transform.position;
        loseScreen.SetActive(true);
        loseScreen.transform.position = positionElement;
    }

    private void WinGame()
    {
        Vector2 positionElement = Camera.main.transform.position;
        winScreen.SetActive(true);
        winScreen.transform.position = positionElement;
    }

    private void OnDisable()
    {
        CommandWinScreen.GetUI -= WinGame;
        CommandLoseGame.GetUI -= LoseGame;
    }

    private void OnEnable()
    {
        CommandWinScreen.GetUI += WinGame;
        CommandLoseGame.GetUI += LoseGame;
    }
}
