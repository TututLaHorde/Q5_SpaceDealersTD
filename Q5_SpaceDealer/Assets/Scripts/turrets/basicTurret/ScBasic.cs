using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBasic : ScTurret
{
    private ScBullet lastBullet;
    
    protected override void Shoot()
    {
        fireDirection.Set(firePoint[0].position.x - myTrans.position.x, firePoint[0].position.y - myTrans.position.y);
        lastBullet = ScArsenal.instance.GiveBasicBullet();
        lastBullet.Fire(fireDirection, bulletSpeed, firePoint[0].position,dammagePerHit);
    }
}
