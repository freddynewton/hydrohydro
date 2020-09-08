using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponUi : MonoBehaviour
{
    public Image weaponImage;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI clipText;

    public void equipweapon(Sprite weaponImg, int bulletAmount, int clipSize)
    {
        weaponImage.sprite = weaponImg;
        weaponImage.preserveAspect = true;

        bulletText.text = bulletAmount.ToString();
        clipText.text = "|" + clipSize.ToString();
    }

    public void setBulletAmount(int bulletAmount, bool animate)
    {
        bulletText.text = bulletAmount.ToString();

        if (!animate) return;

        if (bulletText.gameObject.LeanIsTweening())
            bulletText.gameObject.transform.localScale = Vector3.one;

        LeanTween.scale(bulletText.gameObject, new Vector3(1.3f, 1.3f), 0.1f).setEaseOutQuad();
        LeanTween.scale(bulletText.gameObject, Vector3.one, 0.2f).setEaseOutBounce().setDelay(0.1f);
    }
}
