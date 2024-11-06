using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, 
    IDamageable
{
    [SerializeField] protected float maxHealth;
    [SerializeField] public UnityEvent OnBroken;
    [SerializeField] private Reward reward;
    private float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public virtual void GetDamage(float damage)
    {
        health -= damage;
        //Debug.Log(gameObject.name + " got damage: " + damage +  ". Current health: " + health);
        if(health <= 0) Death();
    }
    public virtual void Death()
    {
        Destroy(gameObject);
        OnBroken?.Invoke();
        int sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        reward.GetFullReward(transform.position, sortingOrder);
    }
}