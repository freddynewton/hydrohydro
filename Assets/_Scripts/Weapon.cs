using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Projectile;

    [Header("Weapon Stats")]
    public float attackRate = 1;
    public float shootKnockback = 0.2f;

    [Header("Screenshake")]
    public bool screenShake;
    public float screenshakeAmount;

    [Header("Projectile Settings")]
    public float projectileForce = 20f;
    public float accuracy = 1;

    //Private
    private float timer;
    private Vector3 mousePos;
    private Camera cameraMain;
    private Rigidbody2D rb;

    private void Start()
    {
        cameraMain = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePos = cameraMain.ScreenToWorldPoint(Input.mousePosition);

        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    public virtual void shoot()
    {
        if (timer <= 0)
        {
            Vector3 dir = mousePos - Inventory.Instance.currentWeapon.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject bullet = Instantiate(Projectile, Inventory.Instance.currentWeapon.transform.position + Vector3.forward, rotation, null) as GameObject;
            
            bullet.GetComponent<Projectile>().speed = projectileForce;

            timer = attackRate;
        }
    }
}
