using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D _rb;
    private Vector2 movement;
    private Animator _animator;
    private bool _canMove = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void StopMovement() => _canMove = false;
    public void ResumeMovement() => _canMove = true;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        if(movement.x > 0 && transform.localScale.x < 0 ||
            movement.x < 0 && transform.localScale.x > 0) Flip();

        if(movement != Vector2.zero)
        {
            _animator.SetBool("isMoving", true);
            _animator.SetFloat("horizontal", movement.x);
            _animator.SetFloat("vertical", movement.y);
        }
        else _animator.SetBool("isMoving", false);
    }

    private void FixedUpdate()
    {
        if(!_canMove) return;
        _rb.MovePosition(_rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }
}
