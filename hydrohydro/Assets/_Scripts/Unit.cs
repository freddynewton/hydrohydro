using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Assign")]
    public GameObject GFX;
    public Stat stats;
    public Animator animator;

    //Hidden values
    [HideInInspector] public int currentHealth;

    //Hidden bools
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        animator = GFX.GetComponent<Animator>();
        currentHealth = stats.health;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
