using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private Collider2D attackRadius;
    [SerializeField] private GameObject arrow;
    [SerializeField] private float damage;
    [SerializeField] private Transform bowPosition;
    private Animator _animator;
    private Timer _attackDelayTimer;
    private float _attackDelay = 2f;
    private bool _canAttack = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _attackDelayTimer = new Timer(this);
        _attackDelayTimer.TimeIsOver += TurnOnCanShoot;
    }

    private void TurnOnCanShoot()
    {
        _canAttack = true;
        attackRadius.enabled = true;
    }

    private void TurnOffCanShoot()
    {
        _canAttack = false;
        attackRadius.enabled = false;
    }

    private void ChangeDirection(Vector2 shootingPos)
    {
        Vector2 direction = shootingPos - (Vector2)transform.position;
        direction.Normalize();
        if(direction.x < 0 && gameObject.transform.localScale.x > 0 ||
            direction.x > 0 && gameObject.transform.localScale.x < 0) Flip();
    }

    private void Flip()
    {
        var localScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(localScale.x*-1, localScale.y, localScale.z);
    }

    private IEnumerator StrikeArrow(Vector2 shootingPos)
    {
        _animator.Play("Shoot Diagonal Up");
        float length  = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length-0.1f);
        GameObject go = Instantiate(arrow, bowPosition.position, Quaternion.identity);
        go.GetComponent<Arrow>().Init(shootingPos, damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Enemy")) return;
        if(!_canAttack) return;
        ChangeDirection(other.transform.position);
        StartCoroutine(StrikeArrow(other.transform.position));
        _attackDelayTimer.StartTimer(_attackDelay);
        TurnOffCanShoot();
    }
}
