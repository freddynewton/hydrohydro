﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public static Playercontroller Instance { get; private set; }

    private Unit unit;
    private Vector2 inputVector;
    private Camera cameraMain;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cameraMain = Camera.main;
        unit = gameObject.GetComponent<Unit>();
    }

    private void Update()
    {
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
        Vector3 worldPos = cameraMain.ScreenToWorldPoint(Input.mousePosition);
        if (worldPos.x <= gameObject.transform.position.x)
            unit.GFX.transform.rotation = Quaternion.Slerp(unit.GFX.transform.rotation, Quaternion.Euler(0, -180, 0), 0.1f);
        else
            unit.GFX.transform.rotation = Quaternion.Slerp(unit.GFX.transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
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
