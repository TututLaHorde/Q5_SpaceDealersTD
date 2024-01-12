using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScArsenal : MonoBehaviour
{
    [SerializeField] GameObject basicBulletGo;

    public static ScArsenal instance;
    private List<ScTurret> arsenal = new List<ScTurret>();
    private List<GameObject> basicBulletGoContainer = new List<GameObject>();
    private Dictionary<GameObject,ScBullet> basicBulletScript = new Dictionary<GameObject,ScBullet>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }

    private void Update()
    {
        foreach (ScTurret turret in arsenal)
        {
            turret.Behave();
        }
    }

    public void GetNewTurret(ScTurret newTurret)
    {
        arsenal.Add(newTurret);
    }

    public ScBullet GiveBasicBullet()
    {
        if (basicBulletGoContainer.Count == 0)
        {
            GameObject tempo = Instantiate(basicBulletGo);
            return tempo.GetComponent<ScBullet>();
        }
        else
        {
            ScBullet result = basicBulletScript[basicBulletGoContainer[0]];
            basicBulletGoContainer[0].SetActive(true);

            basicBulletScript.Remove(basicBulletGoContainer[0]);
            basicBulletGoContainer.Remove(basicBulletGoContainer[0]);

            return result;
        }
    }

    public void GetretiredBullet(GameObject retiredBullet, ScBullet bulletScript, bulletKind bulletKind)
    {
        switch (bulletKind)
        {
            case bulletKind.basic:
                if (!basicBulletGoContainer.Contains(retiredBullet))
                {
                    retiredBullet.SetActive(false);
                    basicBulletGoContainer.Add(retiredBullet);
                    basicBulletScript.Add(retiredBullet, bulletScript);
                }
                break;
        }
    }
}
