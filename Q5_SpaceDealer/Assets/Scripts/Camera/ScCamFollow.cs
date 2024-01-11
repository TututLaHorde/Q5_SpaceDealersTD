using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCamFollow : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    private Transform myTrans;
    private Vector3 newPos;

    private void Start()
    {
        myTrans = transform;
    }


    // Update is called once per frame
    void Update()
    {
        newPos.Set(playerBody.position.x,playerBody.position.y,myTrans.position.z);
        myTrans.position = newPos;
    }
}
