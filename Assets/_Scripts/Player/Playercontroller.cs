using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public static Playercontroller Instance { get; private set; }

    [HideInInspector] public PlayerUnit unit;
    [HideInInspector] public Camera cameraMain;
    [HideInInspector] public Vector3 mousePos;

    [HideInInspector] public Rigidbody2D rb;
    private Vector2 inputVector;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cameraMain = Camera.main;
        unit = gameObject.GetComponent<PlayerUnit>();
    }

    private void Update()
    {
        mousePos = cameraMain.ScreenToWorldPoint(Input.mousePosition);
        movementInputs();
        move();
        LookAtMouse();
    }

 

    private void movementInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 mouseMpos = Input.mousePosition;
        inputVector = new Vector2(moveX, moveY).normalized;

        if (inputVector != Vector2.zero)
            unit.isMoving = true;
        else
            unit.isMoving = false;
    }

    private void move()
    {
        // Check if the payer can move
        if (!unit.canInteract)
        {
            rb.velocity = inputVector * unit.stats.moveSpeed;
        }
    }

    private void LookAtMouse()
    {
        if (mousePos.x <= gameObject.transform.position.x)
        {
            unit.GFX.transform.rotation = Quaternion.Slerp(unit.GFX.transform.rotation, Quaternion.Euler(0, -180, 0), 6f * Time.deltaTime);
        }
        else
        {
            unit.GFX.transform.rotation = Quaternion.Slerp(unit.GFX.transform.rotation, Quaternion.Euler(0, 0, 0), 6f * Time.deltaTime);
        }

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
