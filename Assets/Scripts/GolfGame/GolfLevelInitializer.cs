using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfLevelInitializer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("GolfLevel"))
        {
            PlayerPrefs.SetInt("GolfLevel", 0);
        }
        Instantiate(_levels[PlayerPrefs.GetInt("GolfLevel")], transform);
    }
}
