using UnityEngine;

[RequireComponent (typeof(Collider2D))]

public class ObstacleController : MonoBehaviour
{
    public Collider2D m_coll { get; private set; }

    private void Start()
    {
        m_coll = GetComponent<Collider2D>();
    }
}
