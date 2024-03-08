using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizzProgressUpdater : MonoBehaviour
{
    [SerializeField] private int _quizzIndex;
    [SerializeField] private TMP_Text _percentageText;
    [SerializeField] private Image _progressBar;

    private void OnEnable()
    {
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        var quizz = SaveSystem.LoadData<QuizzSaveData>();
        _percentageText.text = quizz.Percentage[_quizzIndex] + "%";
        _progressBar.fillAmount = quizz.Percentage[_quizzIndex] / 100f;
    }
}
