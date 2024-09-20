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
        if(movement != Vector2.zero)
        {
            _animator.SetFloat("moveX", movement.x);
            _animator.SetFloat("moveY", movement.y);
            _animator.SetBool("isMoving", true);
        }
        else _animator.SetBool("isMoving", false);
    }

    private void FixedUpdate()
    {
        if(!_canMove) return;
        _rb.MovePosition(_rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
