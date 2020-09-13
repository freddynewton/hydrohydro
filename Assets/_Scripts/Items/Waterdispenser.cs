using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Waterdispenser : Interactable
{
    [Header("Water Dispenser")]
    public int waterRefillAmount = 5;
    [SerializeField] private int drinkAmount = 3;
    public Animator animator;
    public ParticleSystem waterParticle;

    public override void Update()
    {
        base.Update();
    }

    public override void interact()
    {
        switch (drinkAmount)
        {
            case 3:
                animator.SetBool("drink_1", true);
                drinkAmount -= 1;
                Playercontroller.Instance.unit.SetWater(waterRefillAmount);
                spawnParticles();
                break;
            case 2:
                animator.SetBool("drink_2", true);
                drinkAmount -= 1;
                Playercontroller.Instance.unit.SetWater(waterRefillAmount);
                spawnParticles();
                break;
            case 1:
                animator.SetBool("drink_3", true);
                drinkAmount -= 1;
                Playercontroller.Instance.unit.SetWater(waterRefillAmount);
                spawnParticles();
                break;
        }


        if (drinkAmount <= 0)
        {
            disableIcon();
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void spawnParticles()
    {
        LeanTween.cancel(gfx);
        waterParticle.Play();

        LeanTween.scaleX(gfx, 1.3f, 0.3f).setEaseOutBounce().setOnComplete(() => {
            LeanTween.scaleX(gfx, 1.0f, 0.3f).setEaseOutBounce();
        });

        LeanTween.scaleY(gfx, 0.7f, 0.3f).setEaseOutBounce().setOnComplete(() => {
            LeanTween.scaleY(gfx, 1.0f, 0.3f).setEaseOutBounce();
        });
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
