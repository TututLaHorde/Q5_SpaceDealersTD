using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public static ObstaclesManager instance;

    [HideInInspector] public List<ObstacleController> m_obstacles = new List<ObstacleController>();

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
            Debug.Log("Twice asteroids manager");
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
            if (child != null && child.TryGetComponent(out ObstacleController asteroid))
            {
                m_obstacles.Add(asteroid);
            }
        }
    }
}
