using UnityEngine;

public class ScResourcesManager : MonoBehaviour
{
    public static ScResourcesManager instance;

    [SerializeField][Min(0f)] private int m_maxMethyl;
    [SerializeField][Min(0f)] private int m_maxMoney;
    private int m_methyl;
    private int m_money;

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
            Debug.Log("Twice resources manager");
            Destroy(this);
            return;
        }
    }

    /*-------------------------------------------------------------------*/

    public int GetMoney()
    {
        return m_money;
    }

    public bool TryLoseMoney(int amount)
    {
        if (m_money - Mathf.Abs(amount) >= 0)
        {
            m_money -= Mathf.Abs(amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GainMoney(int amount)
    {
        m_money += Mathf.Clamp(Mathf.Abs(amount), 0, m_maxMoney);
    }

    public int GetMethyl()
    {
        return m_methyl;
    }

    public bool TryLoseMethyl(int amount)
    {
        if (m_methyl - Mathf.Abs(amount) >= 0)
        {
            m_methyl -= Mathf.Abs(amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GainMethyl(int amount)
    {
        m_methyl += Mathf.Clamp(Mathf.Abs(amount), 0, m_maxMoney);
    }
}
