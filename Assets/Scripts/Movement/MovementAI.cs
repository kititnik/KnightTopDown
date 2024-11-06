using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour
{
    public GameObject UpdateTarget(string[] stalkingTags)
    {
        var cheasableObjects = new List<GameObject>();
        foreach(var targetTag in stalkingTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            foreach(var target in targets)
            {
                cheasableObjects.Add(target);
            }
        }
        float minDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        foreach(var target in cheasableObjects)
        {
            float dist = Vector2.Distance(target.transform.position, transform.position);
            if(dist < minDistance)
            {
                nearestTarget = target;
                minDistance = dist;
            }
        }
        return nearestTarget;
    }
}
