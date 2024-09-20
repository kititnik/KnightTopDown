using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private UnityEvent onHitted;
    [SerializeField] private MeleeDamager meleeDamager;
    [SerializeField] private List<string> cantBrakeTags;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Hitbox hitbox = other.GetComponent<Hitbox>();
        if(cantBrakeTags.Contains(other.tag)) return;
        if(hitbox == null) return;
        float damage = meleeDamager.GetMeleeDamage();
        hitbox.Hit(damage, transform.position);
        onHitted?.Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
