using TreeEditor;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner Instance;
    private Transform m_trs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Twice asteroids spawner");
            Destroy(this);
            return;
        }

        m_trs = transform;
    }

    public void RespawnAsteroid(AsteroidController asteroid)
    {
        Vector3 pos = m_trs.position;

        pos.x += Random.Range(-m_trs.localScale.x / 2f, m_trs.localScale.x / 2f);
        pos.y += Random.Range(-m_trs.localScale.y / 2f, m_trs.localScale.y / 2f);

        asteroid.transform.position = new Vector3(pos.x, pos.y, pos.z);

        asteroid.Init();
    }
}
