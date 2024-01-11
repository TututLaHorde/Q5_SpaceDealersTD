using UnityEngine;
using UnityEngine.Assertions;

public class EnemyLife : MonoBehaviour
{
    [Header("Drop at Death")]
    [SerializeField] private GameObject m_dropPrefab;

    [Header("HP")]
    [SerializeField] private int m_maxlife;
    private int m_currentLife;

    /*-------------------------------------------------------------------*/

    public void TakeDamage(int damage)
    {
        //take dmg
        m_currentLife -= damage;
        m_currentLife = Mathf.Clamp(m_currentLife, 0, m_maxlife);

        //drop & death
        if (m_currentLife == 0)
        {
            GameObject drop = Instantiate(m_dropPrefab, DropManager.instance.transform);
            drop.transform.position = transform.position;

            DropMovement dropMove = drop.GetComponent<DropMovement>();
            Assert.IsNotNull(dropMove, "drop prefab need drop move script");
            DropManager.instance.AddDrop(dropMove);

            GetComponent<EnemyController>().Die();
        }
    }
}
