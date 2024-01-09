using UnityEngine;

public class EnemyMouv : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private float m_speed;

    [Header("Behavior Coeff")]
    [SerializeField][Range(0f, 1f)] private float m_avoidCoeff;
    [SerializeField][Range(0f, 1f)] private float m_cohesionCoeff;

    [Header("Behavior Range")]
    [SerializeField] private float m_avoidingRange;
    [SerializeField] private float m_cohesionRange;
    [SerializeField] private LayerMask m_avoidingLayers;
    [SerializeField] private LayerMask m_cohesionLayers;

    //own components
    private Rigidbody2D m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_rb.velocity += Avoiding();

        //MoveFoward();

        //debug areas
        Debug.DrawRay(transform.position, Vector2.up * m_avoidingRange, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * m_cohesionRange, Color.cyan);
    }

    private void MoveFoward()
    {
        float rot = Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 90f);
        Vector2 vec = new Vector2(Mathf.Cos(rot), Mathf.Sin(rot));
        m_rb.velocity = vec * m_speed;
    }

    private Vector2 Avoiding()
    {
        int nbCloseObj = 0;
        Vector2 avoidVelocity = Vector2.zero;

        //find all object to avoid
        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, m_avoidingRange, m_avoidingLayers))
        {
            if (coll.gameObject != gameObject)
            {
                //specific avoid velocity
                float dist = Vector2.Distance((Vector2)transform.position, coll.ClosestPoint(transform.position));
                Vector2 dir = (Vector2)transform.position - coll.ClosestPoint(transform.position);
                dir = dir.normalized;

                avoidVelocity += dir / dist; //closer is harder
                nbCloseObj++;
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

    private float CohesionAngle()
    {
        return transform.rotation.eulerAngles.z;

        int nbCloseObj = 0;
        Vector2 averagePoints = Vector2.zero;

        //find all object to avoid
        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, m_avoidingRange, m_avoidingLayers))
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

            float o = averagePoints.x - transform.position.x;
            float a = averagePoints.y - transform.position.y;
            float angle = 180 - Mathf.Atan2(o, a) * Mathf.Rad2Deg;
            return angle;
        }

        return transform.rotation.eulerAngles.z;
    }
}
