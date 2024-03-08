using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question", order = 0)]
public class QuestionSO : ScriptableObject
{
    public string Question;
    public List<string> Answers;
    public List<bool> IsCorrect;
}
