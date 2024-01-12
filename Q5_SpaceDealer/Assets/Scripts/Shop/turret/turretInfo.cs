using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "turretInfo")]
public class turretInfo : ScriptableObject
{
    public Sprite sprite;
    public GameObject go;
    public int price;
}
