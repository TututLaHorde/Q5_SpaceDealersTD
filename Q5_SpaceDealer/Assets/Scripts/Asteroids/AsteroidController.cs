using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class AsteroidController : ObstacleController
{
    [SerializeField][Range(0.5f, 5f)] private float m_minScale = 1f;
    [SerializeField][Range(0.5f, 5f)] private float m_maxScale = 1f;
    [SerializeField][Range(0f, 10f)] private float m_minSpeed;
    [SerializeField][Range(0f, 10f)] private float m_maxSpeed;

    private float m_speed;
    private Rigidbody2D m_rb;

    public override void Init()
    {
        if (m_rb == null)
        {
            m_rb = GetComponent<Rigidbody2D>();
        }

        //max is max
        if (m_minScale > m_maxScale)
        {
            m_minScale = m_maxScale;
        }

        if (m_minSpeed > m_maxSpeed)
        {
            m_minSpeed = m_maxSpeed;
        }

        //init
        m_speed = Random.Range(m_minSpeed, m_maxSpeed);
        m_rb.velocity = m_speed * Vector2.left;
        transform.localScale = new Vector3(Random.Range(m_minScale, m_maxScale), Random.Range(m_minScale, m_maxScale), 1f);
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
    }

    private void FixedUpdate()
    {
        m_rb.velocity = Vector2.ClampMagnitude(m_rb.velocity, m_speed);
    }
}
