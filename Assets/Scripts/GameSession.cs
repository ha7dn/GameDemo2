﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int gameSessions = FindObjectsOfType<GameSession>().Length;

        if (gameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);

    }


    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreVal)
    {
        score += scoreVal;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
