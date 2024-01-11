using UnityEngine;

[RequireComponent (typeof(Collider2D))]

public class ObstacleController : MonoBehaviour
{
    public Collider2D m_coll { get; private set; }

    private void Awake()
    {
        m_coll = GetComponent<Collider2D>();
    }

    public virtual void Init()
    {

    }
}
