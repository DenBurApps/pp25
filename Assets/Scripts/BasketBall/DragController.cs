using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private BasketBall _ball;

    [SerializeField] private float _dragLimit = 3f;
    [SerializeField] private float _force = 10f;

    private bool _gameEnded = false;
    private Camera _cam;
    private bool _isDragging;
    private Vector3 _mousePosition 
    {
        get
        {
            Vector3 pos = _cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }

    private void Start()
    {
        _cam = Camera.main;
        _line.positionCount = 2;
        _line.SetPosition(0, Vector2.zero);
        _line.SetPosition(1, Vector2.zero);
        _line.enabled = false;
    }

    public void LockDrag()
    {
        _gameEnded = true;
    }

    private void Update()
    {
        if (!_ball.GetOnGround() || _gameEnded)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0) && !_isDragging)
        {
            DragStart();
        }

        if (_isDragging)
        {
            Drag();
        }

        if(Input.GetMouseButtonUp(0) && _isDragging)
        {
            DragEnd();
        }
    }

    private void DragEnd()
    {
        _isDragging = false;
        _line.enabled = false;

        Vector3 startPos = _line.GetPosition(0);
        Vector3 currentPos = _line.GetPosition(1);

        Vector3 distance = currentPos - startPos;

        Vector3 finalForce = distance * _force;

        _ball.ThrowBall(finalForce);
    }

    private void Drag()
    {
        Vector3 startPos = _line.GetPosition(0);
        Vector3 currentPos = _mousePosition;

        Vector3 distance = currentPos - startPos;

        if(distance.magnitude <= _dragLimit)
        {
            _line.SetPosition(1, currentPos);
        }
        else
        {
            Vector3 limitVector = startPos + (distance.normalized * _dragLimit);
            _line.SetPosition(1, limitVector);
        }


    }

    private void DragStart()
    {
        _isDragging = true;
        _line.enabled = true;
        _line.SetPosition(0, _mousePosition);
    }
}
