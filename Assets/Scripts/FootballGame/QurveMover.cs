using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QurveMover : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private float _speed;
    private float _maxOffset = 2.4f;

    public void MovePoints(float x)
    {
        foreach (var item in _points)
        {
            item.position += new Vector3(x * _speed * Time.deltaTime, 0, 0);
            if (item.position.x >= _maxOffset)
            {
                item.position = new Vector3(_maxOffset, item.position.y, 0);
            }
            if (item.position.x <= -_maxOffset)
            {
                item.position = new Vector3(-_maxOffset, item.position.y, 0);
            }

        }
    }

    public void ResetPoints()
    {
        foreach (var item in _points)
        {
            item.position = new Vector3(0, item.position.y, 0);
        }
    }
}
