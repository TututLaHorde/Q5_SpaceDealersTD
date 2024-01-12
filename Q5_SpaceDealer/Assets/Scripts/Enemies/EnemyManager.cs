using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Aliens Spawner")]
    [SerializeField] private Transform m_spawnArea;
    [SerializeField] private GameObject m_alienPrefab;
    [SerializeField][Range(0, 100)] private int m_nbMaxEnemies;
    [SerializeField][Range(0, 100)] private int m_nbInitalEnemies;

    [Header("Waves")]
    [SerializeField][Min(1f)]   private float m_timeBetweenWaves = 1f;
    [SerializeField][Min(1)]    private int m_maxWaveIndex = 1;
    [SerializeField][Range(0, 50)] private int m_nbEnemiesMinWave;
    [SerializeField][Range(0, 50)] private int m_nbEnemiesMaxWave;
    private int m_waveIndex = 0;
    private Vector2 m_waveOrigin = Vector2.up;

    public List<EnemyController> m_enemies = new List<EnemyController>();
    private HashSet<EnemyController> m_unactiveEnemies = new HashSet<EnemyController>();

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
        //spawn the maximum of enemies unactive
        for(int i = 0; i < m_nbMaxEnemies;  i++) 
        {
            GameObject alien = Instantiate(m_alienPrefab, transform);
            alien.SetActive(false);

            EnemyController enemy = alien.GetComponent<EnemyController>();
            Assert.IsNotNull(enemy);
            m_unactiveEnemies.Add(enemy);
        }

        //wave
        StartCoroutine(SpawnWave(m_nbInitalEnemies));
        StartCoroutine(WaveManager());
    }

    private void FixedUpdate()
    {
        foreach(EnemyController enemy in m_enemies)
        {
            enemy.CallMovements();
        }
    }

    /*-------------------------------------------------------------------*/

    public void AnEnemyDie(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
        m_enemies.Remove(enemy);
        m_unactiveEnemies.Add(enemy);
    }

    /*-------------------------------------------------------------------*/

    private void SpawnAnEnemy()
    {
        if (m_unactiveEnemies.Count > 0 && m_enemies.Count + m_unactiveEnemies.Count <= m_nbMaxEnemies)
        {
            //active enemy
            EnemyController enemy = m_unactiveEnemies.FirstOrDefault();
            m_unactiveEnemies.Remove(enemy);
            m_enemies.Add(enemy);
            enemy.gameObject.SetActive(true);

            //place enemy
            Vector3 pos = Vector3.zero;

            pos.x = m_spawnArea.position.x;
            pos.y = m_spawnArea.position.y;
            pos.x += Random.Range(-m_spawnArea.localScale.x / 2f, m_spawnArea.localScale.x / 2f);
            pos.y += Random.Range(-m_spawnArea.localScale.y / 2f, m_spawnArea.localScale.y / 2f);
            pos.x = m_waveOrigin.x == 0 ? pos.x : m_waveOrigin.x * m_spawnArea.localScale.x * 0.5f;
            pos.y = m_waveOrigin.y == 0 ? pos.y : m_waveOrigin.y * m_spawnArea.localScale.y * 0.5f;

            enemy.transform.position = pos;

            //TODO reset enemy
        }
    }

    private IEnumerator SpawnWave(int nbEnemies)
    {
        //next wave params
        m_waveIndex++;
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                m_waveOrigin = Vector2.up;
                break;
            case 1:
                m_waveOrigin = Vector2.left;
                break;
            case 2:
                m_waveOrigin = Vector2.right;
                break;
            case 3:
                m_waveOrigin = Vector2.down;
                break;
            default:
                Debug.LogAssertion("Random Problem");
                break;
        }

        //active the wave
        for (int i = 0; i < nbEnemies; i++)
        {
            SpawnAnEnemy();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator WaveManager()
    {
        while(true)
        {
            yield return new WaitForSeconds(m_timeBetweenWaves);
            int nbEnemies = Mathf.CeilToInt(Mathf.Lerp(m_nbEnemiesMinWave, m_nbEnemiesMaxWave, (float)m_waveIndex / m_maxWaveIndex));
            print("New wave : " + nbEnemies);
            StartCoroutine(SpawnWave(nbEnemies));
        }
    }
}
