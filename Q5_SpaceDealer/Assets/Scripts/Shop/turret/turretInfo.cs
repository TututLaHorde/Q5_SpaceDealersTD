using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "turretInfo")]
public class turretInfo : ScriptableObject
{
    public float range;
    public Sprite sprite;
    public GameObject go;
    public int price;
}
