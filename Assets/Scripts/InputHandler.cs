using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputHandler : MonoBehaviour
{
    private WizardBehavior wizard;
    private PlayerInput pInput;

    void Awake()
    {
        pInput = GetComponent<PlayerInput>();
        wizard = GetComponent<WizardBehavior>();
    }

    public void OnMove(CallbackContext ctx)
    {
        wizard.SetStickInput(ctx.ReadValue<Vector2>());
    }

    public void OnMark(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            wizard.Mark();
        }
    }

    public void OnTrigger(CallbackContext ctx)
    {
        wizard.Trigger();
    }

}
