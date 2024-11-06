using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PassOrderInLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer otherSpriteRenderer;
    [SerializeField] private int adder;
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = otherSpriteRenderer.sortingOrder+adder;
    }
}
