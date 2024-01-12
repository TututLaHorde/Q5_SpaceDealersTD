using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMiniGun : ScTurret
{
    protected override void Shoot()
    {
        fireDirection.Set(firePoint[0].position.x - myTrans.position.x, firePoint[0].position.y - myTrans.position.y);
        lastBullet = ScArsenal.instance.GiveBasicBullet();
        lastBullet.Fire(fireDirection, bulletSpeed, firePoint[Random.Range(0, firePoint.Count)].position, dammagePerHit);
    }
}
