using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] private TextMeshProUGUI _score, _endScore, _bestScore, _gameOverText, _pausedText;

    [SerializeField] private Button _playButton, _instructionButton, _pauseButton;

    [SerializeField] private GameObject _stoppedPanel;
    [SerializeField] private GameObject[] _birds;
    [SerializeField] private Sprite[] _medals;
    [SerializeField] private Image _medalImage;

    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0f;
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayGame()
    {
        _score.gameObject.SetActive(true);
        _birds[GameController.instance.GetSelectedBird()].SetActive(true);
        _instructionButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()

    {
        if (BirdScript.instance != null)
        {
            if (!BirdScript.instance.isDead)
            {
                _stoppedPanel.SetActive(true);
                _pausedText.gameObject.SetActive(true);

                _gameOverText.gameObject.SetActive(false);
                _pauseButton.gameObject.SetActive(false);

                _endScore.text = "" + BirdScript.instance.score;
                _bestScore.text = "" + GameController.instance.GetHighScore();

                Time.timeScale = 0f;

                _playButton.onClick.RemoveAllListeners();
                _playButton.onClick.AddListener(() => ResumeGame());

            }
        }
    }

    public void StopGameOnDeath(int score)
    {
        _stoppedPanel.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _pausedText.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);

        _endScore.text = "" + score;

        SetHighScore(score);

        _bestScore.text = "" + GameController.instance.GetHighScore();

        SetMedal(score);

        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(() => RestartGame());
    }

    private void SetHighScore(int score)
    {
        if (score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score);
        }
    }

    private void SetMedal(int score)
    {
        if (score > 30)
        {
            _medalImage.sprite = _medals[2];

            if (!GameController.instance.IsRedBirdUnlocked())
            {
                GameController.instance.UnlockRedBird();
            }
        }
        else if (score > 15)
        {
            _medalImage.sprite = _medals[1];
            if (!GameController.instance.IsGreenBirdUnlocked())
            {
                GameController.instance.UnlockGreenBird();
            }
        }
        else if (score > 5)
        {
            _medalImage.sprite = _medals[0];
        }
        else
        {
            _medalImage.sprite = _medals[3];
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _stoppedPanel.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneFader.instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneFader.instance.LoadScene("Main Menu");
    }

    public void SetScore(int score)
    {
        _score.text = "Score: " + score;
    }




}

