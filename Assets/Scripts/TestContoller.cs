using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestContoller : MonoBehaviour
{
    [SerializeField] private int _quizzIndex;
    [SerializeField] private List<QuestionSO> _questions;
    [SerializeField] private TMP_Text _questionCount;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private Transform _answersContainer;
    [SerializeField] private Answer _answerPrefab;
    [SerializeField] private TestResult _testResult;
    private List<Answer> _answers = new List<Answer>();
    private int _currentIndex = 0;
    private int _answerIndex;
    private IEnumerator _showAnswerCoroutine;
    private int _score;

    private void OnEnable()
    {
        _score = 0;
        SetQuestion(0);
    }

    private void OnDisable()
    {
        if(_showAnswerCoroutine != null)
        {
            StopCoroutine(_showAnswerCoroutine);
            _showAnswerCoroutine = null;
        }
    }

    private void SetQuestion(int index)
    {
        _currentIndex = index;
        foreach (var item in _answers)
        {
            Destroy(item.gameObject);
        }
        _answers.Clear();
        _questionCount.text = "Question " + (index + 1) + "/" + _questions.Count;
        _questionText.text = _questions[_currentIndex].Question;
        for (int i = 0; i < _questions[_currentIndex].Answers.Count; i++)
        {
            Answer answer = Instantiate(_answerPrefab, _answersContainer);
            answer.Init(_questions[_currentIndex].Answers[i], i);
            _answers.Add(answer);
        }
        foreach (var item in _answers)
        {
            item.AnswerChoosed += OnAnswerChoosed;
        }
    }

    private void OnAnswerChoosed(int index)
    {
        foreach (var item in _answers)
        {
            item.DeactiveButton();
        }
        _answerIndex = index;
        _showAnswerCoroutine = ShowAnswer();
        StartCoroutine(_showAnswerCoroutine);
    }

    private void TryShowNextQuestion()
    {
        if(_currentIndex + 1 >= _questions.Count)
        {
            ShowResult();
        }
        else
        {
            SetQuestion(_currentIndex + 1);
        }
    }

    private void ShowResult()
    {
        Debug.Log("Show Result");
        gameObject.SetActive(false);
        var quizz = SaveSystem.LoadData<QuizzSaveData>();
        float currentPercentage = ((float)_score / _questions.Count) * 100f;
        if(quizz.Percentage[_quizzIndex] < currentPercentage)
        {
            quizz.Percentage[_quizzIndex] = currentPercentage;
        }
        SaveSystem.SaveData(quizz);
        _testResult.ShowResult(_score, _questions.Count, this);
    }

    private IEnumerator ShowAnswer()
    {
        if (_questions[_currentIndex].IsCorrect[_answerIndex])
        {
            _score++;
            _answers[_answerIndex].ShowCorrect();
        }
        else
        {
            _answers[_answerIndex].ShowWrong();
            //int correctIndex = 0;
            //for (int i = 0; i < _questions[_currentIndex].IsCorrect.Count; i++)
            //{
            //    if (_questions[_currentIndex].IsCorrect[i])
            //    {
            //        correctIndex = i;
            //    }
            //}
            //_answers[correctIndex].ShowCorrect();
        }
        yield return new WaitForSeconds(3);
        TryShowNextQuestion();
    }
}


