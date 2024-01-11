using UnityEngine;

[RequireComponent (typeof(EnemyMove), typeof(Rigidbody2D), typeof(Collider2D))]
[RequireComponent (typeof(EnemyLife))]

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D m_rb { get; private set; }
    public Transform m_trs { get; private set; }
    public Collider2D m_coll {  get; private set; }

    private EnemyMove m_scMove;

    /*-------------------------------------------------------------------*/

    private void OnEnable()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_coll = GetComponent<Collider2D>();
        m_scMove = GetComponent<EnemyMove>();
        m_trs = transform;
    }

    public void CallMovements()
    {
        m_scMove.Move();
    }

    public void Die()
    {
        EnemyManager.instance.AnEnemyDie(this);
    }
}
