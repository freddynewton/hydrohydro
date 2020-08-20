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

    public void animateText(bool crit)
    {   
        gameObject.transform.localScale = Vector2.zero;

        if (crit)
            LeanTween.scale(gameObject, Vector3.one * 1.4f, Lifetime / 2).setEaseOutElastic();
        else
            LeanTween.scale(gameObject, Vector3.one, Lifetime / 2).setEaseOutElastic();

        StartCoroutine(AnimText());
    }

    private IEnumerator AnimText()
    {
        yield return new WaitForSecondsRealtime(Lifetime / 2);
        LeanTween.scale(gameObject, Vector2.zero, Lifetime / 2).setEaseInElastic();
    }
}
