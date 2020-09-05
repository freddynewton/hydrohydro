using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    [Header("PlayerStats")]
    [HideInInspector] public int Level = 1;
    [HideInInspector] public int exp = 0;
    public int maxExp = 10;

    [Header("Water Settings")]
    public float decreaseRate = 3;
    [HideInInspector] public int water = 0;
    public int MaxWater = 100;
    private float waterTick;

    public override void Start()
    {
        base.Start();
        Physics2D.IgnoreLayerCollision(10, 9);

        water = MaxWater;
    }

    public override void Update()
    {
        base.Update();
        waterTicker();
    }

    public override void death(GameObject bullet)
    {
        base.death(bullet);
    }

    private void waterTicker()
    {
        waterTick += Time.deltaTime;

        if (waterTick >= decreaseRate)
        {
            if (water > 0)
                water--;
            else
            {
                currentHealth -= 1;

                StartCoroutine(flashWhite(0.1f));

                DamageNumberPool.Instance.Spawn(gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-0.16f, 0.1f), UnityEngine.Random.Range(0.1f, 0.16f), 0), false, 1);
                if (currentHealth > 0)
                {
                    // Hit anim
                    animator.SetTrigger("hit");
                }
                else
                {
                    // Death Anim
                    death(gameObject);
                }
            }
            
            CanvasManager.Instance.SetWaterSlider(water, MaxWater);
            waterTick = 0;
        }
    }

    public override void DoDamage(GameObject bulletObj, Bullet bulletSettings)
    {
        base.DoDamage(bulletObj, bulletSettings);

        CanvasManager.Instance.SetHealthSlider(currentHealth, stats.health);
    }
}
