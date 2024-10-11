using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void BuildBuilding(Upgradable building)
    {
        _animator.Play("Build");
    }

    public void Chopping()
    {
        _animator.Play("Chop");
    }

    public void StopWork()
    {
        _animator.Play("Idle");
    }
}
