using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("params")]
    [SerializeField][Range(2f, 10f)] private float m_speed;

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
    private Transform m_trs;

    /*-------------------------------------------------------------------*/

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_trs = transform;
    }

    public void Move()
    {
        m_rb.velocity += BoidBehavior();
        m_rb.velocity = Vector2.ClampMagnitude(m_rb.velocity, m_speed);
        LookForward();
    }

    /*-------------------------------------------------------------------*/

    private void LookForward()
    {
        float angle = Vector2.SignedAngle(Vector2.up, m_rb.velocity.normalized);
        m_trs.rotation = Quaternion.Euler(0, 0 , angle);
    }

    private Vector2 BoidBehavior()
    {
        int nbCloseObj = 0;
        int nbAlignObj = 0;
        int nbCohesObj = 0;
        Vector2 avoidVelocity = Vector2.zero;
        Vector2 alignVelocity = Vector2.zero;
        Vector2 cohesionPoint = Vector2.zero;
        Vector2 finalVelocity = Vector2.zero;

        //check other aliens
        foreach (EnemyController enemy in EnemyManager.instance.m_enemies)
        {
            if (enemy.gameObject != gameObject)
            {
                Vector2 closestPoint = enemy.m_trs.position;
                float dist = Vector2.Distance((Vector2)m_trs.position, closestPoint);

                avoidVelocity += OneAvoid(dist, closestPoint, ref nbCloseObj);
                alignVelocity += OneAlign(dist, enemy, ref nbAlignObj);
                cohesionPoint += OneCohes(dist, enemy, ref nbCohesObj);
            }
        }

        //check asteroids
        int aliens = nbCloseObj;
        foreach (ObstacleController obstacle in ObstaclesManager.instance.m_obstacles)
        {
            Vector2 closestPoint = obstacle.m_coll.ClosestPoint(m_trs.position);
            float dist = Vector2.Distance((Vector2)m_trs.position, closestPoint);

            avoidVelocity += OneAvoid(dist, closestPoint, ref nbCloseObj, aliens);
        }

        //calculate the velocity
        finalVelocity += Avoiding(nbCloseObj, avoidVelocity);
        finalVelocity += Alignment(nbAlignObj, alignVelocity);
        finalVelocity += Cohesion(nbCohesObj, cohesionPoint);

        return finalVelocity;
    }

    private Vector2 OneAvoidWall(float dist, Vector2 closestPoint, ref int nbCloseObj)
    {
        if (dist < m_avoidingRange)
        {
            nbCloseObj++;

            //avoid harder if it's closer
            if (dist > 0)
            {
                Vector2 dir = (Vector2)m_trs.position - closestPoint;
                dir = dir.normalized / dist;

                return dir.normalized;
            }
            else
            {
                return new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            }
        }

        return Vector2.zero;
    }

    private Vector2 OneAvoid(float dist, Vector2 closestPoint, ref int nbCloseObj, int nbEnemies = 1)
    {
        if (dist < m_avoidingRange)
        {
            nbCloseObj++;

            //avoid harder if it's closer
            if (dist > 0)
            {
                float ratio = Mathf.Clamp01(dist / m_avoidingRange);
                Vector2 dir = -ratio * (closestPoint - (Vector2)m_trs.position);

                return dir.normalized * (nbEnemies != 0 ? nbEnemies : 1);
            }
            //for supersosition
            else
            {
                return new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            }
        }

        return Vector2.zero;
    }

    private Vector2 OneAlign(float dist, EnemyController enemy, ref int nbAlignObj)
    {
        if (dist < m_alignementRange)
        {
            nbAlignObj++;
            return enemy.m_rb.velocity.normalized;
        }

        return Vector2.zero;
    }

    private Vector2 OneCohes(float dist, EnemyController enemy, ref int nbCohesObj)
    {
        if (dist < m_cohesionRange)
        {
            nbCohesObj++;
            return enemy.m_trs.position;
        }

        return Vector2.zero;
    }

    private Vector2 Avoiding(int nbCloseObj, Vector2 avoidVelocity)
    {
        if (nbCloseObj > 0)
        {
            avoidVelocity /= nbCloseObj;
            avoidVelocity *= m_avoidCoeff;
        }

        return avoidVelocity;
    }

    private Vector2 Alignment(int nbAlignObj, Vector2 alignVelocity)
    {
        //set the necessary direction to avoid
        if (nbAlignObj > 0)
        {
            alignVelocity /= nbAlignObj;
            alignVelocity *= m_alignementCoeff;
        }

        return alignVelocity;
    }

    private Vector2 Cohesion(int nbCohesObj, Vector2 cohesionPoint)
    {
        Vector2 cohesionVeloc = Vector2.zero;

        //set the necessary direction to avoid
        if (nbCohesObj > 0)
        {
            cohesionPoint /= nbCohesObj;

            cohesionVeloc = cohesionPoint - (Vector2)m_trs.position;
            cohesionVeloc.Normalize();
            cohesionVeloc *= m_cohesionCoeff;          
        }

        return cohesionVeloc;
    }
}
