using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public static Playercontroller Instance { get; private set; }

    [HideInInspector] public Unit unit;
    private Vector2 inputVector;
    [HideInInspector] public Camera cameraMain;
    private Rigidbody2D rb;
    private Vector3 worldPos;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cameraMain = Camera.main;
        unit = gameObject.GetComponent<Unit>();
    }

    private void Update()
    {
        worldPos = cameraMain.ScreenToWorldPoint(Input.mousePosition);
        inputs();
        movementInputs();
        move();
        weaponLookAtMouse();
        LookAtMouse();
    }

    private void inputs()
    {
        if (Inventory.Instance.currentWeapon != null && Input.GetKeyDown(KeyCode.Mouse0))
            Inventory.Instance.curWeaponScript.shoot();
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
        if (worldPos.x <= gameObject.transform.position.x)
            unit.GFX.transform.rotation = Quaternion.Slerp(unit.GFX.transform.rotation, Quaternion.Euler(0, -180, 0), 6f * Time.deltaTime);
        else
            unit.GFX.transform.rotation = Quaternion.Slerp(unit.GFX.transform.rotation, Quaternion.Euler(0, 0, 0), 6f * Time.deltaTime);
    }

    private void weaponLookAtMouse()
    {
        
        Vector3 dir = worldPos - Inventory.Instance.currentWeapon.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Inventory.Instance.currentWeapon.transform.rotation = Quaternion.Slerp(Inventory.Instance.currentWeapon.transform.rotation, rotation, 6f * Time.deltaTime);
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
