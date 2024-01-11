using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTurret : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] int dammagePerHit;
    [SerializeField] int health;
    [SerializeField] float fireRate;
    [SerializeField] Transform myTrans;
    [SerializeField] Transform firePoint;

    private Transform target;

    protected void FindTarget()
    {

    }
}
