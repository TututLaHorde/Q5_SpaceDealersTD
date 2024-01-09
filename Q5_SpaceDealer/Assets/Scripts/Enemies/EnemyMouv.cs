using UnityEngine;

public class EnemyMouv : MonoBehaviour
{
    [Header("params")]
    [SerializeField][Range(0f, 10f)] private float m_speed;

    [Header("Behavior Coeff")]
    [SerializeField][Range(0f, 1f)] private float m_avoidCoeff;
    [SerializeField][Range(0f, 0.5f)] private float m_alignementCoeff;
    [SerializeField][Range(0f, 1f)] private float m_cohesionCoeff;

    [Header("Behavior Range")]
    [SerializeField][Range(0f, 10f)] private float m_avoidingRange;
    [SerializeField][Range(0f, 10f)] private float m_alignementRange;
    [SerializeField][Range(0f, 10f)] private float m_cohesionRange;
    [SerializeField] private LayerMask m_avoidingLayers;
    [SerializeField] private LayerMask m_cohesionLayers;

    //own components
    private Rigidbody2D m_rb;

    /*-------------------------------------------------------------------*/

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_rb.velocity += Avoiding();
        m_rb.velocity += Alignment();
        m_rb.velocity += Cohesion();
        m_rb.velocity = m_rb.velocity.normalized * m_speed;
        LookForward();
        //MoveFoward();

        //debug areas
        Debug.DrawRay(transform.position, Vector2.up * m_avoidingRange, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * m_cohesionRange, Color.cyan);
    }

    /*-------------------------------------------------------------------*/

    private void LookForward()
    {
        float angle = Vector2.SignedAngle(Vector2.up, m_rb.velocity.normalized);
        transform.rotation = Quaternion.Euler(0, 0 , angle);
    }

    private Vector2 Avoiding()
    {
        int nbCloseObj = 0;
        Vector2 avoidVelocity = Vector2.zero;

        //find all object to avoid
        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, m_avoidingRange, m_avoidingLayers))
        {
            //specific avoid velocity   
            float dist = Vector2.Distance((Vector2)transform.position, coll.ClosestPoint(transform.position));
            if (dist > 0)
            {           
                Vector2 dir = (Vector2)transform.position - coll.ClosestPoint(transform.position);
                dir = dir.normalized;

                avoidVelocity += dir / dist; //closer is faster
                nbCloseObj++;
            }
            else
            {
                avoidVelocity += new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
            }
        }

        //global avoid velocity
        if (nbCloseObj > 0)
        {
            avoidVelocity /= nbCloseObj;
            avoidVelocity *= m_avoidCoeff;
        }

        return avoidVelocity;
    }

    private Vector2 Cohesion()
    {
        int nbCloseObj = 0;
        Vector2 averagePoints = Vector2.zero;
        Vector2 cohesionVeloc = Vector2.zero;

        //find all object to cohesion
        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, m_cohesionRange, m_cohesionLayers))
        {
            if (coll.gameObject != gameObject)
            {
                averagePoints += coll.ClosestPoint(transform.position);
                nbCloseObj++;
            }
        }

        //set the necessary direction to avoid
        if (nbCloseObj > 0)
        {
            averagePoints /= nbCloseObj;

            cohesionVeloc = averagePoints - (Vector2)transform.position;
            cohesionVeloc.Normalize();
            cohesionVeloc *= m_cohesionCoeff;          
        }

        return cohesionVeloc;
    }

    private Vector2 Alignment()
    {
        int nbCloseObj = 0;
        Vector2 averageVeloc = Vector2.zero;

        //find all object to alignement
        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, m_alignementRange, m_cohesionLayers))
        {
            if (coll.gameObject != gameObject)
            {
                averageVeloc += coll.gameObject.GetComponent<Rigidbody2D>().velocity;
                nbCloseObj++;
            }
        }

        //set the necessary direction to avoid
        if (nbCloseObj > 0)
        {
            averageVeloc /= nbCloseObj;
            averageVeloc *= m_alignementCoeff;
        }

        return averageVeloc;
    }
}
