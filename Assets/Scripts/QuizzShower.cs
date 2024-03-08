using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzShower : MonoBehaviour
{
    [SerializeField] private List<GameObject> _quizes;
    [SerializeField] private List<GameObject> _tests;
    [SerializeField] private Navigator _navigator;

    public void ShowQuizInfo(int index)
    {
        _navigator.HideAll();
        _quizes[index].SetActive(true);
    }

    public void ShowTest(int index)
    {
        _navigator.HideAll();
        _tests[index].SetActive(true);
    }
}
