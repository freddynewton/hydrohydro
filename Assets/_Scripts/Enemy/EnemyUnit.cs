using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyUnit : Unit
{
    [Header("Weapon")]
    public GameObject weapon;
    [HideInInspector] public Weapon weaponScript;
    [HideInInspector] public UtilityAIHandler aiHandler;
    [HideInInspector] public Vector3 targetPos;
    [HideInInspector] public float targetAngle;

    private AIPath aiPath;

    [HideInInspector] public bool canLookAtTarget = true;

    public override void Start()
    {
        base.Start();
        aiPath = gameObject.GetComponent<AIPath>();
        weaponScript = weapon.GetComponent<Weapon>();
        aiHandler = gameObject.GetComponent<UtilityAIHandler>();
    }

    private void lerpWeaponToPos()
    {
        if (weapon != null)
            weapon.transform.position = Vector3.Lerp(weapon.transform.position, weaponPos.transform.position, 50f * Time.deltaTime);
    }

    public override void Update()
    {
        base.Update();
        lookAtVelocity();
        walkAnimation();

        if (weapon != null)
        {
        lerpWeaponToPos();
        weaponLookAtTarget();
        }
    }

    private void weaponLookAtTarget()
    {
        targetPos = Vector3.zero;

        if (aiHandler.target != null)
            targetPos = aiHandler.target.transform.position;
        else
            targetPos = aiPath.velocity;

        Vector3 dir = targetPos - weapon.transform.position;
        targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        if (targetPos.x < gameObject.transform.position.x && canLookAtTarget)
        {
            weapon.GetComponent<SpriteRenderer>().flipY = true;
            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, rotation, stats.weaponLookAtSpeed * Time.deltaTime);
        }
        else
        {
            weapon.GetComponent<SpriteRenderer>().flipY = false;
            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, rotation, stats.weaponLookAtSpeed * Time.deltaTime);
        }
    }

    private void lookAtVelocity()
    {
        if (aiPath.velocity.x < 0 && canLookAtTarget)
        {
            GFX.transform.rotation = Quaternion.Slerp(GFX.transform.rotation, Quaternion.Euler(0, -180, 0), 6f * Time.deltaTime);
        }
        else
        {
            GFX.transform.rotation = Quaternion.Slerp(GFX.transform.rotation, Quaternion.Euler(0, 0, 0), 6f * Time.deltaTime);
        }
    }

    private void walkAnimation()
    {
        if (aiPath.desiredVelocity.x >= 0.1 || aiPath.desiredVelocity.y >= 0.1 || aiPath.desiredVelocity.x <= -0.1 || aiPath.desiredVelocity.y <= -0.1)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    public override void death(GameObject bullet)
    {
        canLookAtTarget = false;
        aiPath.isStopped = true;
        //aiPath.StopAllCoroutines();
        //aiPath.enabled = false;

        if (weapon != null) Destroy(weapon);

        base.death(bullet);

    }

    public override void DoDamage(GameObject bulletObj, Bullet bulletSettings)
    {
        base.DoDamage(bulletObj, bulletSettings);
    }
}
