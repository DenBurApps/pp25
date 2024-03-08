using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballLevelInitializer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FootballLevel"))
        {
            PlayerPrefs.SetInt("FootballLevel", 0);
        }
        Instantiate(_levels[PlayerPrefs.GetInt("FootballLevel")], transform);
    }
}
