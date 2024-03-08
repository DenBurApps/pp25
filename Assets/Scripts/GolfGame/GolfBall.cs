using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private float _maxPower;
    [SerializeField] private float _power;
    [SerializeField] private float _maxGoalSpeed;

    public Action PushBall;
    public Action Goal;

    private bool _isDragging;
    private bool _inHole;

    private void Update()
    {
        PlayerInput();
    }

    private bool _isReady()
    {
        return _rb.velocity.magnitude <= 0.2f;
    }

    private void PlayerInput()
    {
        if (!_isReady())
        {
            return;
        }

        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        if(Input.GetMouseButtonDown(0) && distance <= 0.5f)
        {
            DragStart();
        }
        if(Input.GetMouseButton(0) && _isDragging)
        {
            DragChange(inputPos);
        }
        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            DragRelease(inputPos);
        }
    }

    private void DragStart()
    {
        _isDragging = true;
        _lr.positionCount = 2;
    }

    private void DragChange(Vector2 pos)
    {
        Vector2 dir = (Vector2)transform.position - pos;
        _lr.SetPosition(0, transform.position);
        _lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * _power) / 3f, _maxPower / 3f));
    }

    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        _isDragging = false;
        _lr.positionCount = 0;
        if(distance < 0.5f)
        {
            return;
        }

        Vector2 dir = (Vector2)transform.position - pos;

        _rb.velocity = Vector2.ClampMagnitude(dir * _power, _maxPower);

        StartCoroutine(CheckMovement());
    }

    private IEnumerator CheckMovement()
    {
        while (!_isReady())
        {
            yield return null;
        }
        if (!_inHole)
        {
            PushBall?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Hole hole))
        {
            CheckWinState();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hole hole))
        {
            CheckWinState();
        }
    }

    private void CheckWinState()
    {
        if (_inHole)
        {
            return;
        }

        if(_rb.velocity.magnitude <= _maxGoalSpeed)
        {
            _inHole = true;
            _rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            Goal?.Invoke();
        }
    }
}
