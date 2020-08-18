using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour
{
    [Header("Assign")]
    public GameObject GFX;
    public Stat stats;
    public Animator animator;
    public GameObject weaponPos;

    // private variables
    private SpriteRenderer spriteRend;

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
    }

    public virtual void Update()
    {

    }

    public virtual void DoDamage(int amount, float hitShootKnockback, Vector3 bulletPos)
    {
        currentHealth -= amount;

        StartCoroutine(flashWhite(0.1f));
        
        knockback(bulletPos, hitShootKnockback);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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
        Material baseMat = spriteRend.material;
        spriteRend.material = Resources.Load("Material/White Shader Material") as Material;
        yield return new WaitForSeconds(time);
        StartCoroutine(freezeGame(0.035f));

        spriteRend.material = baseMat;
    }
}
