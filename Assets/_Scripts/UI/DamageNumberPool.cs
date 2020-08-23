using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions.Must;

public class DamageNumberPool : ObjectPool<DamageNumber>
{
    public static DamageNumberPool Instance { get; private set; }

    [SerializeField] private DamageNumberPool pool;

    public void Spawn(Vector3 pos, bool crit, int amount)
    {
        var damageNumber = pool.GetObject();
        damageNumber.transform.position = pos;
        damageNumber.DamageAmount = amount;
        damageNumber.setDamageText(crit);
        damageNumber.gameObject.SetActive(true);
        damageNumber.animateText(crit);
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
