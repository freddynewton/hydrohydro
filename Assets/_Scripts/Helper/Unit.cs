using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class Unit : MonoBehaviour
{
    [Header("Assign")]
    public GameObject GFX;
    public Stat stats;
    public Animator animator;
    public GameObject weaponPos;

    // private variables
    private SpriteRenderer spriteRend;
    private Material baseMat;

    //Hidden Objects
    [HideInInspector] public Rigidbody2D rb;

    //Hidden values
    [HideInInspector] public int currentHealth;

    //Hidden bools
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool canInteract;

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHealth = stats.health;
        animator = GFX.gameObject.GetComponent<Animator>();
        spriteRend = GFX.gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        baseMat = spriteRend.material;
    }

    public virtual void Update()
    {

    }

    public virtual void DoDamage(GameObject bulletObj, Bullet bulletSettings)
    {
        
        bool isCrit = UnityEngine.Random.Range(0.0f, 1.0f) <= bulletSettings.critChance;

        int damage = !isCrit ? bulletSettings.Damage : bulletSettings.Damage * (int)bulletSettings.critMultiplier;
        currentHealth -= damage;

        StartCoroutine(flashWhite(0.1f));

        knockback(bulletObj.transform.position, bulletSettings.hitShootKnockback);

        DamageNumberPool.Instance.Spawn(gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-0.16f, 0.1f), UnityEngine.Random.Range(0.1f, 0.16f), 0), isCrit, damage);
        if (currentHealth > 0)
        {
            // Hit anim
            animator.SetTrigger("hit");
        }
        else
        {
            // Death Anim
            death(bulletObj);
        }

        if (bulletSettings.screenShakeSetting.screenShakeOnHitCharacter)
            CameraHandler.Instance.CameraShake(bulletSettings.screenShakeSetting.duration, bulletSettings.screenShakeSetting.intensitivität, bulletSettings.screenShakeSetting.dropOffTime);
    }

    public virtual void death(GameObject bullet)
    {
        float side = bullet.transform.position.x > gameObject.transform.position.x ? -1 : 1;
        animator.SetTrigger("dead");
        //rb.velocity = Vector3.zero;
        //gameObject.transform.position = gameObject.transform.position;

        gameObject.GetComponent<Collider2D>().enabled = false;
        if (gameObject.layer == 9)
        {
            gameObject.GetComponent<UtilityAIHandler>().enabled = false;
        }


        Vector2 pos = new Vector2(gameObject.transform.position.x + (UnityEngine.Random.Range(0.1f, 0.2f) * side),
            (gameObject.transform.position.y + UnityEngine.Random.Range(-0.2f, 0.2f)));

        LeanTween.moveY(gameObject, pos.y, 1.4f).setEaseOutBounce();
        LeanTween.moveX(gameObject, pos.x, 1.4f).setEaseInSine();
    }

    private void knockback(Vector3 otherPos, float kb)
    {
        rb.AddForce((gameObject.transform.position - otherPos).normalized * kb, ForceMode2D.Force);
    }

    IEnumerator freezeGame(float time)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }

    IEnumerator flashWhite(float time)
    {
        spriteRend.material = Resources.Load("Material/White Shader Material") as Material;
        yield return new WaitForSeconds(time);
        StartCoroutine(freezeGame(0.035f));

        spriteRend.material = baseMat;
    }
}
