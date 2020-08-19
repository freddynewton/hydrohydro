using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public float Lifetime = 2f;
    public Color critColor;
    [HideInInspector] public int DamageAmount;

    [HideInInspector] public TextMeshPro tmp;
    private float lifeTimeElapsed = 0;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        tmp = gameObject.GetComponent<TextMeshPro>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            lifeTimeElapsed += Time.deltaTime;
        }

        if (lifeTimeElapsed > Lifetime)
        {
            lifeTimeElapsed = 0;
            gameObject.SetActive(false);
        }
    }

    public void setDamageText(bool crit)
    {
        tmp.text = DamageAmount.ToString();

        if (crit)
        {
            tmp.faceColor = critColor;

            lifeTimeElapsed -= 1;
        }
        else
        {
            tmp.faceColor = Color.white;
        }
    }

    public void animateText()
    {
        gameObject.transform.localScale = Vector2.zero;
        LeanTween.scale(gameObject, Vector3.one, Lifetime).setEaseOutElastic();
    }
}
