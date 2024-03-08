using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Basket> _baskets;
    private Basket _currentBasket;
    public Action Goal;

    public void SpawnBasket()
    {
        int randomSide = UnityEngine.Random.Range(0, 2);
        int randomMove = UnityEngine.Random.Range(0, 2);
        bool isMove = false;
        if(randomMove == 1)
        {
            isMove = true;
        }
        Basket basket = Instantiate(_baskets[randomSide], _spawnPoints[randomSide]);
        _currentBasket = basket;
        basket.Init(isMove, this);
    }

    public void DestroyBasket()
    {
        Destroy(_currentBasket.gameObject);
    }

    public void GoalMaked()
    {
        Goal?.Invoke();
        SpawnBasket();
    }
}
