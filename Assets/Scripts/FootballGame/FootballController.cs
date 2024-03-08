using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootballController : MonoBehaviour
{
    [SerializeField] private InputControllerFootball _inputControllerFootball;
    [SerializeField] private BallTrajectory _ballTrajectory;
    [SerializeField] private QurveMover _qurveMover;
    [SerializeField] private Ball _ball;
    [SerializeField] private TMP_Text _attemptsText;
    [SerializeField] private int _startAttempts;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;
    private int _currentAttempts;

    private void Start()
    {
        _currentAttempts = _startAttempts;
        _attemptsText.text = _currentAttempts + "/" + _startAttempts;
        _loseScreen.SetActive(false);
        _loseScreen.transform.localScale = Vector3.zero;
        _winScreen.SetActive(false);
        _winScreen.transform.localScale = Vector3.zero;
        StartRound();
    }

    private void OnMiss()
    {
        _ball.Hit -= OnHit;
        _ball.Miss -= OnMiss;
        _ballTrajectory.SetStopStatus(true);
        _currentAttempts--;
        _attemptsText.text = _currentAttempts + "/" + _startAttempts;
        if (_currentAttempts <= 0)
        {
            ShowLoseScreen();
        }
        else
        {
            StartCoroutine(ResetField());
        }

    }

    private void ShowLoseScreen()
    {
        _loseScreen.SetActive(true);
        _loseScreen.transform.DOScale(1, 0.5f).SetLink(gameObject).SetEase(Ease.OutBounce);
    }

    private void ShowWinScreen()
    {
        _winScreen.SetActive(true);
        _winScreen.transform.DOScale(1, 0.5f).SetLink(gameObject).SetEase(Ease.OutBounce);
    }

    private IEnumerator ResetField()
    {
        yield return new WaitForSeconds(2f);
        StartRound();

    }

    public void RestartScene()
    {
        SceneManager.LoadScene("FootballGame");
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnHit()
    {
        _ball.Hit -= OnHit;
        _ball.Miss -= OnMiss;
        if (PlayerPrefs.GetInt("FootballLevel") >= 9)
        {
            PlayerPrefs.SetInt("FootballLevel", 0);
        }
        else
        {
            PlayerPrefs.SetInt("FootballLevel", PlayerPrefs.GetInt("FootballLevel") + 1);
        }
        ShowWinScreen();
    }

    private void StartRound()
    {
        _qurveMover.ResetPoints();
        _inputControllerFootball.SetCanMove(true);
        _inputControllerFootball.StartInputDetection();
        _ballTrajectory.ResetBall();
        _ballTrajectory.SetStopStatus(false);
        _ball.Hit += OnHit;
        _ball.Miss += OnMiss;
    }
}
