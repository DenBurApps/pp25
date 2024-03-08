using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _isMoving;
    [SerializeField] private float _offset;
    [SerializeField] private float _moveTime;
    private Vector2 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        if (_isMoving)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence
            .Append(transform.DOMoveX(_startPosition.x + _offset, _moveTime / 2).SetEase(Ease.Linear))
            .Append(transform.DOMoveX(_startPosition.x - _offset, _moveTime).SetEase(Ease.Linear))
            .Append(transform.DOMoveX(_startPosition.x, _moveTime / 2).SetEase(Ease.Linear));
        mySequence.SetLink(gameObject).SetLoops(-1, LoopType.Restart);
        mySequence.Play();
    }

}
