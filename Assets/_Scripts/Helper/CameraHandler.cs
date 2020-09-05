using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance { get; private set; }
    private Camera mainCam;
    private Vector3 mousePos;

    [HideInInspector] public Transform target;
    public float smoothSpeed = 30;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mainCam = Camera.main;
        mainCam.transparencySortMode = TransparencySortMode.CustomAxis;
        mainCam.transparencySortAxis = mainCam.transform.up;
    }

    private void LateUpdate()
    {
        mainCam.transparencySortAxis = mainCam.transform.up;
    }

    public void CameraShake(float duration, float intensitivität, float dropOffTime)
    {
        shake(duration, intensitivität, dropOffTime);
    }

    public void shake(float duration, float intensitivität, float dropOffTime)
    {
        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, intensitivität, duration)
            .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
            .setLoopClamp()
            .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(gameObject, intensitivität, 0f, dropOffTime).setOnUpdate(
            (float val) =>
            {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutQuad);
    }

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

    }

    private void FixedUpdate()
    {
        Vector3 dir = (mousePos - target.transform.position).normalized / 2;

        offset.x = dir.x;
        offset.y = dir.y;

        if (target != null)
        {
            Vector3 deiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, deiredPosition, (1f - Mathf.Cos(smoothSpeed * Mathf.PI * 0.5f)) * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
