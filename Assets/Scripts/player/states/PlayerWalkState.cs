using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerWalkState : PlayerBaseState
{
    [SerializeField] private float _walkingSpeed = 12f;
    private float _walkingDirection;
    Vector2 _walkingVector;

    public override void StateStart(PlayerStatesManager player)
    {
        stateNumberSwitch = 0;
    }

    public override void StateUpdate(PlayerStatesManager player)
    {
        _walkingDirection = player.input.actions["walk"].ReadValue<float>();
        player.input.onActionTriggered += InputStatesSwitch;
        if (_walkingDirection == 0)
        {
            player.playerRigidbody2D.velocity = Walking(0, 0);
            player.SwitchState(player.idleState);
        }

        switch (stateNumberSwitch)
        {
            case 1: player.SwitchState(player.airState); break;
            case 2: player.SwitchState(player.crouchState); break;
        }
    }

    public override void StateFixedUpdate(PlayerStatesManager player)
    {
        player.playerRigidbody2D.velocity = Walking(_walkingDirection, player.playerRigidbody2D.velocity.y);
    }

    private Vector2 Walking(float direction,float speedCoordY)
    {
       return new Vector2(direction* _walkingSpeed, speedCoordY);
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
