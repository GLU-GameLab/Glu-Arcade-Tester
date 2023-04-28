using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StickTester : MonoBehaviour
{
    Vector3 pos;

    public float scale = 10f;

    private void Start()
    {
        pos = transform.localPosition;
    }

    public void callback(InputAction.CallbackContext context)
    {
        transform.localPosition = pos + ((Vector3)(context.ReadValue<Vector2>() * scale));
    }
}
