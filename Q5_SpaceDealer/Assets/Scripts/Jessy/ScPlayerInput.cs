using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScPlayerInput : MonoBehaviour
{
    private ScMovement movementScript;

    private void Start()
    {
        movementScript = GetComponent<ScMovement>();
    }

    public void GetMovementValues(InputAction.CallbackContext ctxt)
    {
        movementScript.MoveAround(ctxt.ReadValue<Vector2>());
    }
}
