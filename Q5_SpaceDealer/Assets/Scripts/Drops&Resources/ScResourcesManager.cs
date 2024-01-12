using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class ScResourcesManager : MonoBehaviour
{
    public static ScResourcesManager instance;

    [Header("resources")]
    [SerializeField][Min(0f)] private int m_maxMethyl;
    [SerializeField][Min(0f)] private int m_maxMoney;
    private int m_methyl;
    private int m_money;

    [Header("texts")]
    [SerializeField] private TMP_Text m_methyltext;
    [SerializeField] private TMP_Text m_moneytext;

    [Header("conversion")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float minDistanceToConvert;
    [SerializeField] Transform heisenbergTrans;

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

        Assert.IsNotNull(m_methyltext);
        Assert.IsNotNull(m_moneytext);
    }

    private void Start()
    {
        UpdtTextMethyl();
        UpdtTextMoney();
    }

    private void Update()
    {
        if (Vector3.Distance(playerPos.position, heisenbergTrans.position) < minDistanceToConvert)
            ConvertMethylToCash();
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
            UpdtTextMoney();
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
        UpdtTextMoney();
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
            UpdtTextMethyl();
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
        UpdtTextMethyl();
    }

    /*-------------------------------------------------------------------*/

    private void UpdtTextMoney()
    {
        m_moneytext.text = m_money + " M $";
    }

    private void UpdtTextMethyl()
    {
        m_methyltext.text = m_methyl.ToString();
    }

    /*-------------------------------------------------------------------*/

    private void ConvertMethylToCash()
    {
        GainMoney(m_methyl / 1000);
        m_methyl = 0;
        UpdtTextMethyl();
    }
}
