using UnityEngine;

public class DropMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField][Range(0f, 10f)] private float m_maxSpeed;
    [SerializeField][Range(0f, 10f)] private float m_minSpeed;
    [SerializeField] private float m_destructionDist;
    private Vector3 direction = Vector3.zero;

    [Header("Methyl Amount")]
    [SerializeField][Min(0f)] private float m_recupDist;
    [SerializeField][Min(0)] private int m_maxMethyl;
    [SerializeField][Min(0)] private int m_minMethyl;
    private int m_methylAmount;

    [HideInInspector] public Transform m_targetTrs;
    private Transform m_trs;

    /*-------------------------------------------------------------------*/


    private void OnEnable()
    {
        m_methylAmount = Random.Range(m_minMethyl, m_maxMethyl + 1);
        direction.x = Random.Range(-1f, 1f);
        direction.y = Random.Range(-1f, 1f);
        m_trs = transform;
    }

    /*-------------------------------------------------------------------*/

    public void Move()
    {
        //follow player
        if (IsInCollect())
        {
            FollowTarget();
        }
        //space move
        else
        {
            m_trs.position += m_minSpeed * direction * Time.deltaTime;
        }

        //if it's too far of the center
        DestroyArea();
    }

    public bool IsInCollect()
    {
        return m_targetTrs != null;
    }

    /*-------------------------------------------------------------------*/

    private void DestroyArea()
    {
        if (m_trs.position.magnitude >= m_destructionDist) 
        {
            DropManager.instance.RemoveDrop(this);
        }
    }

    private void FollowTarget()
    {
        direction = m_targetTrs.position - m_trs.position;
        m_trs.position += m_maxSpeed * (direction.normalized + direction) * Time.deltaTime;

        //recup
        if ((m_trs.position - m_targetTrs.position).magnitude <= m_recupDist)
        {
            ScResourcesManager.instance.GainMethyl(m_methylAmount);
        }
    }
}
