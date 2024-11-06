using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtentionMethods
{
    public static GameObject InstantiateWithSortingOrder(GameObject original, Transform parent, int orderInLayer)
    {
        GameObject inst = GameObject.Instantiate(original, parent);
        SpriteRenderer sr = inst.GetComponent<SpriteRenderer>();
        if(sr != null) sr.sortingOrder = orderInLayer;
        return inst;
    }

    public static GameObject InstantiateWithSortingOrder(GameObject original, Vector2 position, int orderInLayer)
    {
        GameObject inst = GameObject.Instantiate(original, position, Quaternion.identity);
        SpriteRenderer sr = inst.GetComponent<SpriteRenderer>();
        if(sr != null) sr.sortingOrder = orderInLayer;
        return inst;
    }
}
