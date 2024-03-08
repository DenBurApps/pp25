using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrajectory : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    [SerializeField] private Transform _p0;
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private Transform _p3;
    [SerializeField] private int _segmentsNumber;
    [SerializeField] private float _ballMoveDuration;
    [SerializeField] private LineRenderer _lineRenderer;

    [Range(0, 1)]
    [SerializeField] private float _t;

    private bool _isStoped = false;

    private void Start()
    {
        _lineRenderer.positionCount = _segmentsNumber + 1;
    }

    void Update()
    {
        _ball.position = Bezier.GetPoint(_p0.position, _p1.position, _p2.position, _p3.position, _t);
        DrawTrajectory();
        
    }

    public void SetStopStatus(bool enable)
    {
        _isStoped = enable;
    }

    public void ResetBall()
    {
        _t = 0f;
    }

    private void DrawTrajectory()
    {
        for (int i = 0; i < _segmentsNumber + 1; i++)
        {
            float parameter = (float)i / _segmentsNumber;
            Vector3 point = Bezier.GetPoint(_p0.position, _p1.position, _p2.position, _p3.position, parameter);
            _lineRenderer.SetPosition(i, point);
        }
    }

    public void PushBall()
    {
        StartCoroutine(ChangeValueOverTime());
    }

    private IEnumerator ChangeValueOverTime()
    {
        float timeElapsed = 0f;

        while (timeElapsed < _ballMoveDuration)
        {
            if (_isStoped)
            {
                yield break;
            }
            _t = Mathf.Lerp(0, 1, timeElapsed / _ballMoveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _t = 1;
    }


}
