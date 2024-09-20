using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private UnityEvent onBroken;

    public void GetDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " got damage: " + damage +  ". Current health: " + health);
        if(health <= 0) Death();
    }
    public void Death()
    {
        Destroy(gameObject);
        onBroken?.Invoke();
    }
}