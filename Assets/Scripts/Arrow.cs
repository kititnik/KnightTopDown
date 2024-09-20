using UnityEngine;

public class Arrow : MeleeDamager
{
    [SerializeField] private float _timeToTarget;
    private Vector2 _targetPosition;
    private Vector2 _startPosition;
    private float _elapsedTime = 0f;
    private float _damage;

    public override float GetMeleeDamage()
    {
        return _damage;
    }

    public void Init(Vector2 targetPos, float damage)
    {
        _startPosition = transform.position;
        _targetPosition = targetPos;
        _damage = damage;
    }

    private void Update()
    {
        if(_startPosition == null || _targetPosition == null) return;
        _elapsedTime += Time.deltaTime;

        float t = _elapsedTime / _timeToTarget;

        if (t > 1f)
        {
            t = 1f;
            Destroy(gameObject, 1f);
        }

        float x = Mathf.Lerp(_startPosition.x, _targetPosition.x, t);

        float peakHeight = 1f;
        float y = Mathf.Lerp(_startPosition.y, _targetPosition.y, t) + peakHeight * Mathf.Sin(t * Mathf.PI);

        transform.position = new Vector2(x, y);
    }
}
