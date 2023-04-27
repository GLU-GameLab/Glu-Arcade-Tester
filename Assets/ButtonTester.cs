using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonTester : MonoBehaviour
{
    public Color Off, On;

    public Image Image;

    private void Start()
    {
        Image.color = Off;
    }
    public void Callback(InputAction.CallbackContext context)
    {
        Image.color = (context.control.IsPressed()) ? On : Off;
    }
}
