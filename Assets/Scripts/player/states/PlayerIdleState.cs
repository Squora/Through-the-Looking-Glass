using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerIdleState : PlayerBaseState
{
    public override void StateStart(PlayerStatesManager player)
    {
        stateNumberSwitch = 0;
    }

    public override void StateUpdate(PlayerStatesManager player)
    {
        player.input.onActionTriggered += InputStatesSwitch;
        if (player.input.actions["walk"].ReadValue<float>() != 0)
        {
            player.SwitchState(player.walkState);
        }
        switch (stateNumberSwitch)
        {
            case 1: player.SwitchState(player.airState); break;
            case 2: player.SwitchState(player.crouchState); break;
        }
    }

    public override void StateFixedUpdate(PlayerStatesManager player)
    {

    }

    private void InputStatesSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.action.name)
            {
                case "jump": stateNumberSwitch = 1; break;

                case "crouch": stateNumberSwitch = 2; break;
            }
        }
    }
}
