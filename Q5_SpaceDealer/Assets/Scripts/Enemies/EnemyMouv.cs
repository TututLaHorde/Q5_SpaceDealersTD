using System.Collections.Generic;
using UnityEngine;

public class EnemyMouv : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private float m_speed;

    [Header("FOV")]
    [SerializeField] private float m_avoidingArea;
    [SerializeField] private float m_cohesionArea;
    [SerializeField] private LayerMask m_avoidingLayers;
    [SerializeField] private LayerMask m_cohesionLayers;

    //own components
    private Rigidbody2D m_rb;

    //utilities
    private List<GameObject> m_avoidingGO;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Avoiding();
        Cohesion();
        MoveFoward();

        //debug areas
        Debug.DrawRay(transform.position, Vector2.up * m_avoidingArea, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * m_avoidingArea, Color.cyan);
    }

    private void MoveFoward()
    {
        float rot = Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 90f);
        Vector2 vec = new Vector2(Mathf.Cos(rot), Mathf.Sin(rot));
        m_rb.velocity = vec * m_speed;
    }

    private void Avoiding()
    {
        //reset previous check
        m_avoidingGO.Clear();

        //find all object to avoid
        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, m_avoidingArea, m_avoidingLayers))
        {
            if (coll.gameObject != gameObject)
            {
                m_avoidingGO.Add(coll.gameObject);
            }
        }
    }

    private void Cohesion()
    {

    }
}
