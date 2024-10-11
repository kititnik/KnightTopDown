using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElevationEntry : MonoBehaviour
{
    [SerializeField] private int newOrderInLayer;
    [SerializeField] private int newLayer;
    [SerializeField] private GameObject endPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stalkingMovement = collision.gameObject.GetComponent<StalkingMovement>();
        if(stalkingMovement != null) stalkingMovement.SetObjectToGoToPosition(endPos.transform.position);
        collision.gameObject.layer = newLayer;
        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = newOrderInLayer;
    }
}
