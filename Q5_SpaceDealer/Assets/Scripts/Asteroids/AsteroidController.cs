using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class AsteroidController : ObstacleController
{
    [Header("Size")]
    [SerializeField][Range(0.5f, 5f)] private float m_minScale = 1f;
    [SerializeField][Range(0.5f, 5f)] private float m_maxScale = 1f;
    [SerializeField] private float m_massByMetre = 1f;

    [Header("Speed")]
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

        //forme
        float scaleX = Random.Range(m_minScale, m_maxScale);
        float scaleY = Random.Range(m_minScale, m_maxScale);
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
        m_rb.mass = m_massByMetre * scaleX * scaleY;
        
    }

    private void FixedUpdate()
    {
        m_rb.velocity = Vector2.ClampMagnitude(m_rb.velocity, m_speed);
    }
}
