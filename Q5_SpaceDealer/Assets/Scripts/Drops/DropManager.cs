using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager instance;

    [SerializeField] private GameObject m_dropPrefab;
    [SerializeField][Min(0)] private int m_startingNumb;

    [HideInInspector] public List<DropMovement> m_drops = new();
    private List<DropMovement> m_unactiveDrops = new();

    /*-------------------------------------------------------------------*/

    private void Awake()
    {
        //singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Twice drop manager");
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        CreateUnactiveDrop(m_startingNumb);
    }

    private void FixedUpdate()
    {
        foreach (var drop in m_drops)
        {
            drop.Move();
        }
    }

    /*-------------------------------------------------------------------*/

    public DropMovement AddDrop()
    {
        if (m_unactiveDrops.Count == 0)
        {
            CreateUnactiveDrop(10);
        }

        DropMovement drop = m_unactiveDrops[0];
        m_unactiveDrops.RemoveAt(0);
        m_drops.Add(drop);

        return drop;
    }

    public void RemoveDrop(DropMovement dropMove)
    {
        m_drops.Remove(dropMove);
        m_unactiveDrops.Add(dropMove);
    }

    /*-------------------------------------------------------------------*/

    private void CreateUnactiveDrop(int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            GameObject dropInstace = Instantiate(m_dropPrefab, transform);
            DropMovement dropMove = dropInstace.GetComponent<DropMovement>();
            dropInstace.SetActive(false);

            m_unactiveDrops.Add(dropMove);
        }
    }
}
