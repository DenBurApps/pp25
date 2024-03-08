using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _onGround = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Ground ground))
        {
            _onGround = true;
        }
    }

    public bool GetOnGround()
    {
        return _onGround;
    }

    public void ThrowBall(Vector3 force)
    {
        _rb.AddForce(-force, ForceMode2D.Impulse);
        _onGround = false;
    }
}
