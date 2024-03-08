using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestResult : MonoBehaviour
{
    [SerializeField] private GameObject _resultCanvas;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _resultImage;
    [SerializeField] private Sprite _goodSprite;
    [SerializeField] private Sprite _badSprite;
    [SerializeField] private string _goodResult;
    [SerializeField] private string _goodDescription;
    [SerializeField] private string _badResult;
    [SerializeField] private string _badDescription;
    private TestContoller _testContoller;

    public void ShowResult(int correctAnswers, int questionsCount, TestContoller testController)
    {
        _testContoller = testController;
        if (correctAnswers > questionsCount / 2)
        {
            _resultText.text = _goodResult;
            _description.text = _goodDescription;
        }
        else
        {
            _resultText.text = _badResult;
            _description.text = _badDescription;
        }
        _resultCanvas.SetActive(true);
    }

    public void RestartTest()
    {
        _resultCanvas.SetActive(false);
        _testContoller.gameObject.SetActive(true);
    }
}
