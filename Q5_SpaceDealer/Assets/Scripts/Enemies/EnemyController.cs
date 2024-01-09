using UnityEngine;

[RequireComponent (typeof(EnemyMove))]

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D m_rb { get; private set; }
    public Transform m_trs { get; private set; }

    private EnemyMove m_scMove;

    /*-------------------------------------------------------------------*/

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_scMove = GetComponent<EnemyMove>();
        m_trs = transform;
    }

    public void CallMovements()
    {
        m_scMove.Move();
    }
}
