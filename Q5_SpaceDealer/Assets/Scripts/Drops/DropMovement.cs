using UnityEngine;

public class DropMovement : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float m_maxSpeed;
    [SerializeField][Range(0f, 10f)] private float m_minSpeed;
    [SerializeField] private float m_destructionDist;
    private Vector3 direction = Vector3.zero;

    private Transform m_targetTrs;
    private Transform m_trs;

    /*-------------------------------------------------------------------*/

    private void Start()
    {
        m_trs = transform;
    }

    private void OnEnable()
    {
        direction.x = Random.Range(-1f, 1f);
        direction.y = Random.Range(-1f, 1f);
    }

    /*-------------------------------------------------------------------*/

    public void Move()
    {
        //follow player
        if (m_targetTrs != null)
        {
            direction = m_trs.position - m_targetTrs.position;
            m_trs.position += m_maxSpeed * direction * Time.deltaTime;
        }
        //space move
        else
        {
            m_trs.position += m_minSpeed * direction * Time.deltaTime;
        }

        //if it's too far of the center
        DestroyArea();
    }

    /*-------------------------------------------------------------------*/

    private void DestroyArea()
    {
        if (m_trs.position.magnitude >= m_destructionDist) 
        {
            DropManager.instance.RemoveDrop(this);
        }
    }
}
