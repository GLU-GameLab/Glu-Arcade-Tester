using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;


public struct ArcadeGamepadState : IInputStateTypeInfo
{
    // In the case of a HID (which we assume for the sake of this demonstration),
    // the format will be "HID". In practice, the format will depend on how your
    // particular device is connected and fed into the input system.
    // The format is a simple FourCC code that "tags" state memory blocks for the
    // device to give a base level of safety checks on memory operations.
    public FourCC format => new FourCC('H', 'I', 'D');

    [InputControl(name = "BlueBottom", layout = "Button", bit = 0, offset = 3)]
    [InputControl(name = "BlueTop", layout = "Button", bit = 1, offset = 3)]
    [InputControl(name = "GreenBottom", layout = "Button", bit = 2, offset = 3)]
    [InputControl(name = "GreenTop", layout = "Button", bit = 3,offset = 3)]
    [InputControl(name = "YellowBottom", layout = "Button", bit = 4, offset = 3)]
    [InputControl(name = "YellowTop", layout = "Button", bit = 5, offset = 3)]
    public int Buttons;


    [InputControl(name = "Stick", layout = "Vector2", offset = 1, sizeInBits = 16,format ="")]
    [InputControl(name = "Stick/x", layout = "Axis")]
    public short x;
    [InputControl(name = "Stick/y", layout = "Axis")]
    public short y;

}


#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[InputControlLayout(displayName = "arcade Gamepad ", stateType = typeof(ArcadeGamepadState))]
public class ArcadeGamepad : InputDevice
{
    [InputControl]
    public ButtonControl GreenTop { get; private set; }
    [InputControl]
    public ButtonControl GreenBottom { get; private set; }

    [InputControl]
    public ButtonControl YellowTop { get; private set; }
    [InputControl]
    public ButtonControl YellowBottom { get; private set; }

    [InputControl]
    public ButtonControl BlueTop { get; private set; }
    [InputControl]
    public ButtonControl BlueBottom { get; private set; }

    [InputControl]
    public Vector2Control Stick { get; private set; }

    // Register the device.
    static ArcadeGamepad()
    {

        // In case you want instance of your device to automatically be created
        // when specific hardware is detected by the Unity runtime, you have to
        // add one or more "device matchers" (InputDeviceMatcher) for the layout.
        // These matchers are compared to an InputDeviceDescription received from
        // the Unity runtime when a device is connected. You can add them either
        // using InputSystem.RegisterLayoutMatcher() or by directly specifying a
        // matcher when registering the layout.
        return;
        InputSystem.RegisterLayout<ArcadeGamepad>(matches: new InputDeviceMatcher()
        .WithInterface("HID")
        .WithManufacturer("xin-mo.com"));
    }

    [MenuItem("Tools/Add costum gamepad")]
    public static void Init()
    {
        InputSystem.AddDevice<ArcadeGamepad>();

    }

    // This is only to trigger the static class constructor to automatically run
    // in the player.
    [RuntimeInitializeOnLoadMethod]
    private static void InitializeInPlayer() { }

    protected override void FinishSetup()
    {
        GreenTop = GetChildControl<ButtonControl>("GreenTop");
        GreenBottom = GetChildControl<ButtonControl>("GreenBottom");
        YellowTop = GetChildControl<ButtonControl>("YellowTop");
        YellowBottom = GetChildControl<ButtonControl>("YellowBottom");
        BlueTop = GetChildControl<ButtonControl>("BlueTop");
        BlueBottom = GetChildControl<ButtonControl>("BlueBottom");
        Stick = GetChildControl<Vector2Control>("Stick");
        base.FinishSetup();
    }
}