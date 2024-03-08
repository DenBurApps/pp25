using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Basket : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _lockCollider;
    [SerializeField] private float _moveOffset;

    private BasketSpawner _basketSpawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BasketBall basketBall))
        {
            _lockCollider.enabled = false;
            StartCoroutine(LockBasket());
        }
    }

    public void Init(bool isMove, BasketSpawner basketSpawner)
    {
        Vector2 startPos = transform.localPosition;
        _basketSpawner = basketSpawner;
        if (isMove)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence
                .Append(transform.DOLocalMoveY(startPos.x - _moveOffset, 2).SetEase(Ease.Linear))
                .Append(transform.DOLocalMoveY(startPos.x, 2).SetEase(Ease.Linear));
            mySequence.SetLink(gameObject).SetLoops(-1, LoopType.Restart);
            mySequence.Play();
        }
    }

    private IEnumerator LockBasket()
    {
        yield return new WaitForSeconds(0.5f);
        _lockCollider.enabled = true;
        _basketSpawner.GoalMaked();
        Destroy(gameObject);
    }
}
