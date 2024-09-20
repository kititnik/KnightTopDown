using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICombatOperation
{
    public bool StartOperation(Action onOperationEnded, Transform spawnPoint);
}
