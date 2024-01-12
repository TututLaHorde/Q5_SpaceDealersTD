using UnityEngine;

public class ScCollectDrop : MonoBehaviour
{
    [SerializeField][Min(0f)] private float m_colletRange;
    [SerializeField] private LayerMask m_colletLayers;

    private void Update()
    {
        foreach (var coll in Physics2D.OverlapCircleAll(transform.position, m_colletRange, m_colletLayers))
        {
            if (coll.TryGetComponent(out DropMovement drop) && !drop.IsInCollect())
            {
                drop.m_targetTrs = transform;
            }
        }
    }
}
