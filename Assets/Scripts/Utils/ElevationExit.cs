using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElevationExit : MonoBehaviour
{
    [SerializeField] private int newOrderInLayer;
    [SerializeField] private int newLayer;
    [SerializeField] private int graphIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.layer = newLayer;
        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = newOrderInLayer;
    }
}
