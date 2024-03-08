using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameShower : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameInfos;
    [SerializeField] private Navigator _navigator;

    public void LoadFootballGame()
    {
        SceneManager.LoadScene("FootballGame");
    }

    public void LoadGolfGame()
    {
        SceneManager.LoadScene("GolfGame");
    }

    public void LoadBasketballGame()
    {
        SceneManager.LoadScene("BasketballGame");
    }

    public void ShowGameInfo(int index)
    {
        _navigator.HideAll();
        _gameInfos[index].SetActive(true);
    }
}
