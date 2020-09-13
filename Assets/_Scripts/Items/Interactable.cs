using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    public float interactRange = 0.5f;
    public GameObject icon;
    public GameObject gfx;

    [Header("Interact Key")]
    public KeyCode key = KeyCode.E;

    private bool inRange = false;

    public abstract void interact();

    public virtual void Awake()
    {
        addCircleCol();
    }

    public virtual void Update()
    {
        if (inRange && Input.GetKeyDown(key)) interact();
    }

    private void addCircleCol()
    {
        CircleCollider2D col = gameObject.AddComponent<CircleCollider2D>();
        col.radius = interactRange;
        col.isTrigger = true;

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;    
    }

    private void showIcon()
    {
        icon.SetActive(true);
        LeanTween.scale(icon, Vector3.one, 1f).setEaseOutBounce().setOnComplete(() => {
            LeanTween.scale(icon, Vector3.one * 0.8f, 0.5f).setEaseInOutQuart().setLoopPingPong();
        });
    }

    public void disableIcon()
    {
        LeanTween.cancel(icon);
        LeanTween.scale(icon, Vector3.zero, 0.3f).setEaseInCirc().setOnComplete(() =>
        {
            icon.SetActive(false);
        });
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
            showIcon();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
            disableIcon();
        }
    }
}
