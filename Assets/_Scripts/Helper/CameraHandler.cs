using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [HideInInspector] public Transform target;
    public float smoothSpeed = 30;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 deiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, deiredPosition, (1f - Mathf.Cos(smoothSpeed * Mathf.PI * 0.5f)) * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
