using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Objects")]
    public Bullet bullet;
    public GameObject muzzleFlash;
    private Animator muzzleAnimator;
    private Animator animator;
    private BulletPool bulletPool;

    [Header("Weapon Stats")]
    public float attackRate = 1;
    public int damage = 1;
    [Range(0, 1)] public float critChance = 0.05f;
    public int critMultiplier = 2;

    [Header("Weapon Bonus Stats")]
    public float shootKnockback = 25f;
    public float hitShootKnockback = 40f;

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

    private void Awake()
    {
        bullet.Damage = damage;
        bullet.hitShootKnockback = hitShootKnockback;
        bullet.critChance = critChance;
        bullet.critMultiplier = critMultiplier;
    }

    private void Start()
    {
        cameraMain = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        bulletPool = GetComponent<BulletPool>();
        animator = GetComponent<Animator>();
        muzzleAnimator = muzzleFlash.GetComponent<Animator>();
    }

    private void Update()
    {
        mousePos = cameraMain.ScreenToWorldPoint(Input.mousePosition);

        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    public virtual void shoot(float Angle)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("shoot");
            muzzleAnimator.SetTrigger("shoot");
            bulletPool.SpawnBullet(bullet, Inventory.Instance.currentWeapon.transform.position, Inventory.Instance.currentWeapon.transform.right, Angle);

            Playercontroller.Instance.rb.AddForce((Playercontroller.Instance.gameObject.transform.position - mousePos) * shootKnockback);
  
            spawnBulletshell();

            timer = attackRate;
        }
    }

    public void spawnBulletshell()
    {
        GameObject shell = Instantiate(Resources.Load("Items/bulletshell"), gameObject.transform.position, Quaternion.identity, null) as GameObject;

        float side = mousePos.x > Playercontroller.Instance.transform.position.x ? -1 : 1;

        Vector2 pos = new Vector2((gameObject.transform.position.x + (Random.Range(0.1f, 0.4f) * side)),
            (gameObject.transform.position.y + Random.Range(-0.2f, 0.2f)));

        LeanTween.moveY(shell, pos.y, 1.4f).setEaseOutBounce();
        LeanTween.moveX(shell, pos.x, 1.4f).setEaseInSine();
        LeanTween.rotateLocal(shell, new Vector3(0, 0, Random.Range(-360, 360)), 1.4f).setEaseInOutElastic();

    }
}
