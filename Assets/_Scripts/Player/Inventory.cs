﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public GameObject currentWeapon;
    [HideInInspector] public Weapon curWeaponScript;

    // Start is called before the first frame update
    void Start()
    {
        equipWeapon();
    }

    public void equipWeapon()
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
        currentWeapon = Instantiate(Resources.Load("Items/Weapons/Assault-Rifle-001"), Playercontroller.Instance.gameObject.transform.position + new Vector3(0.03f, -0.03f), Quaternion.identity, Inventory.Instance.gameObject.transform) as GameObject;
        curWeaponScript = currentWeapon.GetComponent<Weapon>();
        curWeaponScript.controlledByPlayer = true;
        CanvasManager.Instance.WeaponUi.equipweapon(currentWeapon.GetComponent<SpriteRenderer>().sprite, curWeaponScript.CurrentBulletAmount, curWeaponScript.ClipSize);
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
