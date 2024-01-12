using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScMovement : MonoBehaviour
{
    [Header("parameters")]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxSpeed;

    private Rigidbody2D rb;
    private Transform myTrans;
    private Vector2 newPosition;
    private Vector2 direction;
    private Vector3 rotationAngle;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myTrans = transform;
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rb.AddForce(direction * speed);
            LimitSpeed();
            TurnAround();
            rb.freezeRotation = true;
        }
    }

    public void MoveAround(Vector2 movementVector)
    {
        direction = movementVector;

        if (direction == Vector2.zero)
        {
            rb.freezeRotation = false;
            rb.drag = 0.5f;
        }
        else
            rb.drag = 0f;
    }

    private void LimitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = (rb.velocity / rb.velocity.magnitude) * maxSpeed;
        }
    }

    private void TurnAround()
    {
        float zAngle = Vector2.Angle(Vector2.right, direction);

        if (direction.y < 0)
        {
            zAngle = 360 - zAngle;
        }

        transform.rotation = Quaternion.Euler(0,0,zAngle - 90);
    }
}