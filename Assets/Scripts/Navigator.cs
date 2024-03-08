using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _canvases;
    [SerializeField] private List<SpriteSwitcher> _setImages;

    private void Awake()
    {
        ShowMenu();
    }

    public void HideAll()
    {
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
    }

    public void ShowMenu()
    {
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[0].SetActive(true);
        foreach (var item in _setImages)
        {
            item.SwitchSprite(false);
        }
        _setImages[0].SwitchSprite(true);
    }

    public void ShowGames()
    {
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[1].SetActive(true);
        foreach (var item in _setImages)
        {
            item.SwitchSprite(false);
        }
        _setImages[1].SwitchSprite(true);
    }

    public void ShowQuizz()
    {
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[2].SetActive(true);
        foreach (var item in _setImages)
        {
            item.SwitchSprite(false);
        }
        _setImages[2].SwitchSprite(true);
    }

    public void ShowSettings()
    {
        foreach (var item in _canvases)
        {
            item.SetActive(false);
        }
        _canvases[3].SetActive(true);
        foreach (var item in _setImages)
        {
            item.SwitchSprite(false);
        }
        _setImages[3].SwitchSprite(true);
    }
}
