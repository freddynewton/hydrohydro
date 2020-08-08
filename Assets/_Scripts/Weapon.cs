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

    //Timer
    private float timer;

    private void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    public virtual void shoot()
    {
        if (timer <= 0)
        {
            GameObject bullet = Instantiate(Projectile, Inventory.Instance.currentWeapon.transform.position, Inventory.Instance.currentWeapon.transform.rotation, null) as GameObject;
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            rbBullet.AddForce(Inventory.Instance.currentWeapon.transform.right * projectileForce, ForceMode2D.Impulse);
        }
    }
}
