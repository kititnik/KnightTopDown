using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamageable
{
    void GetDamage(float damage);
    void Death();
}
