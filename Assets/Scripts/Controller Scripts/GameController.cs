using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private const string PREFS_HIGH_SCORE = "High Score";
    private const string PREFS_SELECTED_BIRD = "Selected Bird";
    private const string PREFS_RED_BIRD_UNLOCKED = "Red Bird";
    private const string PREFS_GREEN_BIRD_UNLOCKED = "Green Bird";



    private void Awake()
    {
        MakeSingleton();
        IsGameStartedFirst();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void IsGameStartedFirst()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedFirst"))
        {
            PlayerPrefs.SetInt(PREFS_HIGH_SCORE, 0);
            PlayerPrefs.SetInt(PREFS_SELECTED_BIRD, 0);
            PlayerPrefs.SetInt(PREFS_RED_BIRD_UNLOCKED, 0);
            PlayerPrefs.SetInt(PREFS_GREEN_BIRD_UNLOCKED, 0);
            PlayerPrefs.SetInt("IsGameStartedFirst", 0);
        }
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(PREFS_HIGH_SCORE, score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(PREFS_HIGH_SCORE);
    }

    public void SetSelectedBird(int selectedBird)
    {
        PlayerPrefs.SetInt(PREFS_SELECTED_BIRD, selectedBird);
    }

    public int GetSelectedBird()
    {
        return PlayerPrefs.GetInt(PREFS_SELECTED_BIRD);
    }

    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(PREFS_RED_BIRD_UNLOCKED, 1);
    }

    public bool IsRedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(PREFS_RED_BIRD_UNLOCKED) == 1;
    }

    public void UnlockGreenBird()
    {
        PlayerPrefs.SetInt(PREFS_GREEN_BIRD_UNLOCKED, 1);
    }

    public bool IsGreenBirdUnlocked()
    {
        return PlayerPrefs.GetInt(PREFS_GREEN_BIRD_UNLOCKED) == 1;
    }
}
