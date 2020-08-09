using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        weaponLookAtMouse();
        weaponInputs();
        lerpWeaponOnPos();
    }

    private void lerpWeaponOnPos()
    {
        Inventory.Instance.currentWeapon.transform.position = Vector3.Lerp(Inventory.Instance.currentWeapon.transform.position, Playercontroller.Instance.unit.weaponPos.transform.position, 50f * Time.deltaTime);
    }

    private void weaponInputs()
    {
        if (Inventory.Instance.currentWeapon != null && Input.GetKey(KeyCode.Mouse0))
            Inventory.Instance.curWeaponScript.shoot();
    }

    private void weaponLookAtMouse()
    {

        Vector3 dir = Playercontroller.Instance.mousePos - Inventory.Instance.currentWeapon.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Playercontroller.Instance.mousePos.x <= gameObject.transform.position.x)
        {
            Inventory.Instance.currentWeapon.GetComponent<SpriteRenderer>().flipY = true;
            Inventory.Instance.currentWeapon.transform.rotation = Quaternion.Slerp(Inventory.Instance.currentWeapon.transform.rotation, rotation, 200f * Time.deltaTime);
        }
        else
        {
            Inventory.Instance.currentWeapon.GetComponent<SpriteRenderer>().flipY = false;
            Inventory.Instance.currentWeapon.transform.rotation = Quaternion.Slerp(Inventory.Instance.currentWeapon.transform.rotation, rotation, 200f * Time.deltaTime);
        }
    }
}
