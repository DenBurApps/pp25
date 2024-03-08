using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerFootball : MonoBehaviour
{
    [SerializeField] private QurveMover _qurveMover;
    [SerializeField] private BallTrajectory _ballTrajectory;
    private Vector2 _startTouchPosition;
    private bool _isMoved = false;
    private bool _canMove = false;

    public void StartInputDetection()
    {
        StartCoroutine(InputDetector());
    }

    public void SetCanMove(bool enable)
    {
        _canMove = enable;
    }

    private IEnumerator InputDetector()
    {
        while (_canMove)
        {
            yield return null;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.position.y < Screen.height / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _startTouchPosition = touch.position;
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        _isMoved = true;
                        _qurveMover.MovePoints(touch.deltaPosition.x);
                    }
                    if (touch.phase == TouchPhase.Ended && _isMoved)
                    {
                        _isMoved = false;
                        _ballTrajectory.PushBall();
                        _canMove = false;
                        Debug.Log("Push");
                    }

                }
            }
        }
    }
}
