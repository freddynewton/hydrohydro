using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Container class for bullet settings
/// </summary>
[Serializable]
public class BulletPoolSettings
{
    [Tooltip("How many bullets to spawn")] public int NumBullets;
    [Tooltip("The prefab to use when spawning the bullet")] public Transform BulletPF;
}

/// <summary>
/// A pool of bullets
/// </summary>
public class BulletPool : MonoBehaviour
{
    /// <summary>
    /// Container class for a bullet object
    /// </summary>
    private class BulletContainer
    {
        public Bullet Settings;
        public Transform Transform;
        public Vector3 PrevPos;
        public float Time;
        public int WaitTimer = 20;
        public Vector2 SizeCollide;
        public int Damage;
        public float hitShootKnockback;
        public float Angle;
        public bool Disable = false;
        public float critChance;
        public float critMultiplier;
    }

    [SerializeField, Tooltip("Should this object create bullets on load?")] private bool autoInit = true;
    [SerializeField, Tooltip("A list of bullet settings to use")] private List<BulletPoolSettings> settings;

    private bool _initialised = false;

    // The current bullets that have been spawned
    private Dictionary<BulletPoolSettings, List<BulletContainer>> _bullets;

    private void Awake()
    {
        if (autoInit)
            Init();
    }

    public void Init()
    {
        _bullets = new Dictionary<BulletPoolSettings, List<BulletContainer>>();

        // spawn the bullets
        foreach (var setting in settings)
        {
            _bullets.Add(setting, new List<BulletContainer>());

            for (int i = 0; i < setting.NumBullets; ++i)
            {
                var t = Instantiate(setting.BulletPF, Inventory.Instance.transform);
                t.gameObject.SetActive(false);
                _bullets[setting].Add(new BulletContainer { Transform = t, Time = 0 });
            }
        }

        _initialised = true;
    }


    private void Update()
    {
        // if we're not initialised, return immediately
        if (!_initialised) return;

        foreach (var setting in settings)
        {
            // for each bullet
            for (int i = setting.NumBullets - 1; i >= 0; --i)
            {
                // cache the bullet container so we don't have to do multiple lookups
                var bulletContainer = _bullets[setting][i];
                if (bulletContainer.Settings == null) continue;

                // Increment the bullet container's current time
                bulletContainer.Time += Time.deltaTime;

                var elapsedTime = bulletContainer.Time;
                // if it has been alive too long, destroy it
                if (elapsedTime > bulletContainer.Settings.Lifetime)
                    bulletContainer.Transform.gameObject.SetActive(false);

                // if it's not active
                if (!bulletContainer.Transform.gameObject.activeSelf)
                {
                    // add to the wait timer, so we don't spawn it before it has properly reset
                    ++bulletContainer.WaitTimer;
                    continue;
                }

                // store the previous position, so we can check in a capsule spanning the whole distance it has travelled
                bulletContainer.PrevPos = bulletContainer.Transform.position;

                if (elapsedTime >= 0)
                {
                    // move the bullet forwards
                    bulletContainer.Transform.position +=
                        bulletContainer.Transform.right * Time.deltaTime * bulletContainer.Settings.Speed;

                    bulletContainer.Transform.localScale = bulletContainer.Settings.Prefab.localScale * bulletContainer.Settings.Size;
                }
            }
        }
    }

    /// <summary>
    /// Create a bullet
    /// </summary>
    public void SpawnBullet(Bullet bulletSettings, Vector3 position, Vector3 direction, float angle)
    {
        // find a bullet where the settings match and the bullet is inactive and ready to load
        var bulletsArray = _bullets.First(setting => setting.Key.BulletPF == bulletSettings.Prefab).Value;
        var idx = bulletsArray.FindIndex(bullet => bullet.Transform.gameObject.activeSelf == false && bullet.WaitTimer > 15);

        // if there isn't a bullet, exit out and log a warning so we know to increase the pool
        if (idx == -1)
        {
            Debug.LogWarning($"No bullets left on {gameObject.name}");
            return;
        }

        // initialise the bullet
        bulletsArray[idx].Transform.position = position;
        bulletsArray[idx].Transform.right = direction;
        bulletsArray[idx].PrevPos = position;
        bulletsArray[idx].Time = 0;
        bulletsArray[idx].WaitTimer = 0;
        bulletsArray[idx].Transform.gameObject.SetActive(true);
        bulletsArray[idx].Settings = bulletSettings;
        bulletsArray[idx].Angle = angle;
        bulletsArray[idx].Disable = false;
    }

    /// <summary>
    /// For each physics step
    /// </summary>
    private void FixedUpdate()
    {
        // if we're not initialised, exit out straight away
        if (!_initialised) return;


        foreach (var setting in settings)
        {
            for (int i = 0; i < setting.NumBullets; ++i)
            {
                // cache the bullet container so we don't have to do multiple lookups
                var bulletContainer = _bullets[setting][i];

                // if the bullet isn't active or it's got 0 scale, skip it
                if (!bulletContainer.Transform.gameObject.activeSelf || bulletContainer.Transform.localScale == Vector3.zero) continue;

                // check objects in a capsule based on the bullet's position, its previous position and the radius, using the physics collision matrix

                var found = Physics2D.OverlapBox(bulletContainer.Transform.position, bulletContainer.SizeCollide, bulletContainer.Angle, bulletContainer.Settings.CollideWith);

                /*
                var found = Physics.OverlapCapsule(bulletContainer.Transform.position, bulletContainer.PrevPos,
                bulletContainer.Settings.Size, bulletContainer.Settings.CollideWith, QueryTriggerInteraction.Ignore);
                */

                // if there's no collisions or all collisions are trigger objects, skip it
                if (found == null) continue;

                // Hit Effect
                switch (found.gameObject.layer)
                {
                    // Enemies
                    case 9:
                        found.gameObject.GetComponent<Unit>().DoDamage(bulletContainer.Transform.gameObject, bulletContainer.Settings);
                        break;

                    // Player
                    case 10:
                        found.gameObject.GetComponent<Unit>().DoDamage(bulletContainer.Transform.gameObject, bulletContainer.Settings);
                        break;
                }

                GameObject impact = Instantiate(bulletContainer.Settings.ImpactEffect, bulletContainer.Transform.position, Quaternion.AngleAxis(bulletContainer.Angle, Vector3.forward), null) as GameObject;
                Destroy(impact, bulletContainer.Settings.ImpactEffectDuration);


                bulletContainer.Disable = true;
                bulletContainer.Transform.gameObject.SetActive(false);
            }
        }
    }
}