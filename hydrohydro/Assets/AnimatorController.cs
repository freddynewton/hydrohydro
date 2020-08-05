using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private PlayerUnit unit;

    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.GetComponent<PlayerUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        unit.animator.SetBool("isMoving", unit.isMoving);
    }
}
