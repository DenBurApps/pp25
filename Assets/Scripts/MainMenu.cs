using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _dailyNews;

    public void ShowDailyNews()
    {
        _dailyNews.SetActive(true);
    }

    public void HideDailyNews()
    {
        _dailyNews.SetActive(false);
    }
}
