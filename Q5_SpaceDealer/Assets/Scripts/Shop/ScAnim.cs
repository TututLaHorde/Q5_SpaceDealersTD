using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAnim : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ShopInOut(string boolName)
    {
        animator.SetBool(boolName, !animator.GetBool(boolName));
    }
}
