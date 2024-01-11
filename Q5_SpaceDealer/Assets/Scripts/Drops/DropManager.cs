using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager instance;


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

    /*-------------------------------------------------------------------*/

    public void AddDrop(DropMovement dropMove)
    {

    }

    public void RemoveDrop(DropMovement dropMove)
    {
    }
}
