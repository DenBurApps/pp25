using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GolfController : MonoBehaviour
{
    [SerializeField] private TMP_Text _lifesText;
    [SerializeField] private int _maxLifes;
    [SerializeField] private GolfBall _golfBall;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;

    private int _currentLifes;
    private bool _isWin = false;
    private bool _isLose = false;

    private void Start()
    {
        _currentLifes = _maxLifes;
        _lifesText.text = _currentLifes + "/" + _maxLifes;
    }

    private void OnDisable()
    {
        _golfBall.PushBall -= OnPushBall;
        _golfBall.Goal -= OnGoal;
    }

    private void OnEnable()
    {
        _golfBall.PushBall += OnPushBall;
        _golfBall.Goal += OnGoal;
    }

    public void OnPushBall()
    {
        _currentLifes--;
        _lifesText.text = _currentLifes + "/" + _maxLifes;
        if(_currentLifes <= 0 && !_isWin)
        {
            _isLose = true;
            ShowLoseScreen();
        }
    }

    public void OnGoal()
    {
        if (!_isLose)
        {
            if(PlayerPrefs.GetInt("GolfLevel") >= 9)
            {
                PlayerPrefs.SetInt("GolfLevel", 0);
            }
            else
            {
                PlayerPrefs.SetInt("GolfLevel", PlayerPrefs.GetInt("GolfLevel") + 1);
            }
            _isWin = true;
            ShowWinScreen();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GolfGame");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainScene");
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
}
