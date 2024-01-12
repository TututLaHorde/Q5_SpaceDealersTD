using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBullet : MonoBehaviour
{
    [SerializeField] bulletKind myKind;
    private Rigidbody2D rb;
    private GameObject go;
    private Transform myTrans;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTrans = transform;
        go = gameObject;
    }

    public void Fire(Vector2 direction, float force, Vector3 originalPos)
    {
        myTrans.position = originalPos;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void Retire()
    {
        ScArsenal.instance.GetretiredBullet(go,this,myKind);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            Retire();
        }
    }
}

public enum bulletKind
{
    basic,
}
