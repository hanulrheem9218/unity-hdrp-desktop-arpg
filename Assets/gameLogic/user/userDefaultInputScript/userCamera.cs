using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.Find("player").transform;

    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
        transform.LookAt(target);
    }
}
