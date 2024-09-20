using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null) return;
        if(transform.position == target.position) return;
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
