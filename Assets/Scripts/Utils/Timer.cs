using System.Collections;
using System;
using UnityEngine;

public class Timer
{
    private float _time;
    private float _remainingTime;

    private IEnumerator _countdown;

    private MonoBehaviour _context;

    public event Action<float> HasBeenUpdated;
    public event Action TimeIsOver;

    public Timer(MonoBehaviour context) => _context = context;

    public void StartTimer(float time)
    {
        _time = time;
        _remainingTime = _time;
        _countdown = Countdown();
        _context.StartCoroutine(_countdown);
    }
    
    public void StopTimer()
    {
        if (_countdown != null) _context.StopCoroutine(_countdown);
    }

    public void RestartTimer(float time)
    {
        StopTimer();
        StartTimer(time);
    }

    private IEnumerator Countdown()
    {
        while (_remainingTime >= 0)
        {
            _remainingTime -= Time.deltaTime;
            
            HasBeenUpdated?.Invoke(_remainingTime);
            
            yield return null;
        }
        TimeIsOver?.Invoke();
    }
}
