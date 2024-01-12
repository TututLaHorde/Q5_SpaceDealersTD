using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTurret : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int dammagePerHit;
    [SerializeField] int health;
    [SerializeField] float shotPerSeconde; // number of shoots per seconde
    [SerializeField] protected Transform myTrans;
    [SerializeField] protected List<Transform> firePoint = new List<Transform>();
    [SerializeField] private LayerMask enemies;

    private Transform target;
    private GameObject targetGo;
    private EnemyManager enemyManager = EnemyManager.instance;
    private Collider2D[] enemiesInRange;
    private Vector3 directionToTarget;
    protected Vector2 fireDirection;

    private float lastShotTimeMark;
    private float fireRate;


    private void Start()
    {
        fireRate = 1f / shotPerSeconde;
        ScArsenal.instance.GetNewTurret(this);
        lastShotTimeMark = Time.realtimeSinceStartup - fireRate;
        FindTarget();
    }

    private bool Canshoot()
    {
        if (Time.realtimeSinceStartup >= lastShotTimeMark + fireRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected bool FindTarget()
    {
        if (target == null)
        {
            enemiesInRange = Physics2D.OverlapCircleAll(myTrans.position, range, enemies);
            if (enemiesInRange.Length > 0)
            {
                target = enemiesInRange[0].transform;
                targetGo = target.gameObject;
                return true;
            }
            return false;
        }
        else
        {
            if (Vector3.Distance(target.position, myTrans.position) > range || !targetGo.activeInHierarchy)
            {
                target = null;
                targetGo = null;
                return false;
            }
            return true;
        }
    }
    protected void Aim()
    {
        directionToTarget = target.position - myTrans.position;
        float zAngle = Vector2.Angle(Vector2.right, directionToTarget);

        if (directionToTarget.y < 0)
        {
            zAngle = 360 - zAngle;
        }

        transform.rotation = Quaternion.Euler(0, 0, zAngle);
    }
    protected virtual void Shoot()
    {
        
    }

    
    public void Behave()
    {
        if (target != null)
        {
            Aim();
        }
            

        if (Canshoot())
        {
            if (FindTarget())
            {
                Shoot();
                lastShotTimeMark = Time.realtimeSinceStartup;
            }
        }
    }


    public float GetRange()
    {
        return range;
    }
}
