using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElevationEntry : MonoBehaviour
{
    [SerializeField] private int newOrderInLayer;
    [SerializeField] private int newLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger) return;
        collision.gameObject.layer = newLayer;
        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = newOrderInLayer;
    }
}
