using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasketBallController : MonoBehaviour
{
    [SerializeField] private BasketSpawner _basketSpawner;
    [SerializeField] private BasketBall _basketBall;
    [SerializeField] private DragController _dragController;
    [SerializeField] private TMP_Text _prepareText;
    [SerializeField] private Image _progressBar;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TMP_Text _finalScoreText;
    private int _currentScore = 0;

    private int _startPrepareSeconds = 3;

    private void Start()
    {
        Prepare();
    }

    private void OnEnable()
    {
        _basketSpawner.Goal += UpdateScore;
    }

    private void OnDisable()
    {
        _basketSpawner.Goal += UpdateScore;
    }

    private void Prepare()
    {
        _scoreText.text = _currentScore.ToString();
        _prepareText.text = _startPrepareSeconds.ToString();
        Sequence mySequence = DOTween.Sequence();
        mySequence
            .Append(_prepareText.transform.DOScale(1, 0.8f))
            .Append(_prepareText.transform.DOScale(0, 0.2f));
        mySequence.SetLink(gameObject).SetLoops(3, LoopType.Restart).OnStepComplete(() => 
        {
            _startPrepareSeconds--;
            _prepareText.text = _startPrepareSeconds.ToString();
        }).OnComplete(() => 
        {
            _basketSpawner.SpawnBasket();
            Timer();
        });
        mySequence.Play();
    }

    private void UpdateScore()
    {
        _currentScore++;
        _scoreText.text = _currentScore.ToString();
    }

    private void Timer()
    {
        _progressBar.DOFillAmount(0, 80).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(() => 
        {
            EndGame();
        });
    }

    private void EndGame()
    {
        _dragController.LockDrag();
        _basketSpawner.DestroyBasket();
        _finalScoreText.text = "SCORE: " + _currentScore;
        _gameUI.SetActive(false);
        _gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("BasketballGame");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainScene");
    }
}
