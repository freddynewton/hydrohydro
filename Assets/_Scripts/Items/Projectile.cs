﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float speed;
    public Sprite muzzleFlash;

    public int framesToFlash = 3;
    public float destroyTime = 3;

    private SpriteRenderer spriteRend;
    private Sprite defaultSprite;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRend.sprite;

        StartCoroutine(FlashMuzzleFlash());
    }

    IEnumerator FlashMuzzleFlash()
    {
        spriteRend.sprite = muzzleFlash;

        for (int i = 0; i < framesToFlash; i++)
        {
            yield return 0;
        }

        spriteRend.sprite = defaultSprite;
    }

    public void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        switch (col.gameObject.tag)
        {
            case "Obstacle":
                Destroy(gameObject);
                break;
            case "Enemy":
                break;
        }
    }

    private void Awake()
    {
        Destroy(gameObject, destroyTime);
    }
}
