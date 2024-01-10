using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<EnemyController> m_enemies = new List<EnemyController>();

    /*-------------------------------------------------------------------*/

    private void Awake()
    {
        //singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Twice enemy manager");
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        //get all ennemies in children
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child != null && child.TryGetComponent(out EnemyController enemy))
            {
                m_enemies.Add(enemy);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach(EnemyController enemy in m_enemies)
        {
            enemy.CallMovements();
        }
    }

    /*-------------------------------------------------------------------*/
}
