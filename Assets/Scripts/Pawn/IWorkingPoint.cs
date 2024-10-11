using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IWorkingPoint
{
    public GameObject GetWorkingPlace();
    public WorkTypes GetWorkType();
    public UnityEvent GetOnWorkEndedEvent();
    public void StartWork();
}
