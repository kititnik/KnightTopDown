using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public interface ICombatOperation
{
    public bool StartOperation(float difficulty, Action onOperationEnded, Transform[] spawnPoints);
}
