using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulettShell : MonoBehaviour
{
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(12, Physics2D.AllLayers);
        Physics2D.IgnoreLayerCollision(12, 8, true);
    }
}
