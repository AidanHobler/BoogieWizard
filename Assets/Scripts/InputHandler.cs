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

    public void OnActivate(CallbackContext ctx)
    {
        OSCManager.instance.SendTrigger(new Tile(2, 2));
    }

}
