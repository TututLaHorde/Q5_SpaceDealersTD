using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int m_maxlife;
    private int m_currentLife;

    /*-------------------------------------------------------------------*/

    private void OnEnable()
    {
        m_currentLife = m_maxlife;
    }

    /*-------------------------------------------------------------------*/

    public void TakeDamage(int damage)
    {
        //take dmg
        m_currentLife -= damage;
        m_currentLife = Mathf.Clamp(m_currentLife, 0, m_maxlife);

        //drop & death
        if (m_currentLife == 0)
        {     
            DropMovement dropMove = DropManager.instance.AddDrop();
            dropMove.transform.position = transform.position;          

            GetComponent<EnemyController>().Die();
        }
    }
}
