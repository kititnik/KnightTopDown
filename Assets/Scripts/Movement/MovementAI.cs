using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour
{
    public Transform UpdateTarget(string[] stalkingTags)
    {
        List<Transform> cheasableObjects = new List<Transform>();
        foreach(var targetTag in stalkingTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            foreach(var target in targets)
            {
                cheasableObjects.Add(target.transform);
            }
        }
        float minDistance = Mathf.Infinity;
        Transform nearestTarget = null;
        foreach(var target in cheasableObjects)
        {
            float dist = Vector2.Distance(target.position, transform.position);
            if(dist < minDistance)
            {
                nearestTarget = target;
                minDistance = dist;
            }
        }
        return nearestTarget;
    }
}
