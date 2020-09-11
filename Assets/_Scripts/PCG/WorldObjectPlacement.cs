using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PCG/WorldObjectPlacement", fileName = "WorldObjectPlacement")]
public class WorldObjectPlacement : ScriptableObject
{
    [Header("Size")]
    public int width;
    public int height;


    [Header("Objects")]
    public GameObject[] objects;



}
