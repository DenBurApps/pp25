using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_Text _answer;
    [SerializeField] private Button _button;
    [SerializeField] private Color _correctColor;
    [SerializeField] private Color _wrongColor;

    private int _index;

    public Action<int> AnswerChoosed;

    public void Init(string answer, int index)
    {
        _answer.text = answer;
        _index = index;
        _button.onClick.AddListener(OnAnswerButtonClicked);
    }

    public void DeactiveButton()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void ShowCorrect()
    {
        _answer.color = _correctColor;
    }

    public void ShowWrong()
    {
        _answer.color = _wrongColor;
    }

    private void OnAnswerButtonClicked() 
    {
        AnswerChoosed?.Invoke(_index);
    }

}
