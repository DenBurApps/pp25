using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizzSaveData : SaveData
{
    public List<float> Percentage { get; set; }

    public QuizzSaveData(List<float> percentage)
    {
        Percentage = percentage;
    }
}
