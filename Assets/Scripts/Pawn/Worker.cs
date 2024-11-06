using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Worker : MonoBehaviour
{
    [SerializeField] private UnityEvent onStartedBuilding;
    [SerializeField] private UnityEvent onStoppedWorking;
    private Animator _animator;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void BuildBuilding(Upgradable building)
    {
        _animator.Play("Build");
        onStartedBuilding?.Invoke();
    }

    public void Chopping()
    {
        _animator.Play("Chop");
    }

    public void Mine()
    {
        gameObject.SetActive(false);
    }

    public void StopWork()
    {
        gameObject.SetActive(true);
        _animator.Play("Idle");
        onStoppedWorking?.Invoke();
    }
}
