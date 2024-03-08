using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Action Miss;
    public Action Hit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy enemy))
        {
            Miss?.Invoke();
        }
        else
        {
            Hit?.Invoke();
        }
    }
}
