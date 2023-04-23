using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float _speed;

    [SerializeField] 
    private float _waitingTimeBeforePatroll;

    [SerializeField]
    private List<Transform> _points ;

    private Transform _startPoint;
    private Transform _endPoint;

    private int _countEndPosition = 1;
    private int _countStartPosition = 0;
    private float _currentTime ;
    private float _timer;
    private bool _isTimerOver = true;
    // Start is called before the first frame update
    private  void Start()
    {
        GetStartPosition(_countStartPosition);
        GetSecondPosition(_countEndPosition);
    }
    // Update is called once per frame
    private void Update()
    {
        if (_isTimerOver)
        {
            _currentTime += Time.deltaTime;
            var distance = Vector3.Distance(_startPoint.position, _endPoint.position);
            var travelTime = distance / _speed;
            var progress = _currentTime / travelTime;
            var  newPosition= Vector3.Lerp(_startPoint.position, _endPoint.position, progress );
            gameObject.transform.position = newPosition; 
            if (transform.position == _endPoint.position)
            {
                _countStartPosition ++;
                _countEndPosition ++;
                if (_countEndPosition == 5)
                {
                    _countStartPosition = 0;
                    _countEndPosition = 1;
                }
                GetStartPosition(_countStartPosition);
                GetSecondPosition(_countEndPosition);
                _currentTime = 0f;
                WaitingWhenTimerIsOver();
            }
        }
        else
        {
            WaitingWhenTimerIsOver();
        }
    }
   private void GetStartPosition(int i )
    {
        _startPoint = _points[i];
    }
    private void GetSecondPosition(int i)
    {
        _endPoint = _points[i];
    }
    private void WaitingWhenTimerIsOver()
    {
        if (_timer < _waitingTimeBeforePatroll)
        {
            _timer += Time.deltaTime;
            _isTimerOver = false;
            Debug.Log(_timer.ToString());
        }
        else if (_timer >= _waitingTimeBeforePatroll)
        {
            _timer = 0;
            _isTimerOver = true;
            Debug.Log(_timer.ToString());
            return;
        }
    }
}
