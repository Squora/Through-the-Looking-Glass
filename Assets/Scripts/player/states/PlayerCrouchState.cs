using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCrouchState : PlayerBaseState
{
    [SerializeField] private float _walkingSpeed;
    private float _walkingDirection;
    //Vector2 walkingVector;
    private Transform _spriteTransform;
    public override void StateStart(PlayerStatesManager player)
    {
        _spriteTransform = transform.GetComponentInChildren<Transform>();
        _spriteTransform.localScale = new Vector3(_spriteTransform.localScale.x, _spriteTransform.localScale.y / 2);
        transform.position = transform.position - _spriteTransform.localScale.y / 2 * Vector3.up;
        stateNumberSwitch = 0;
    }
    public override void StateUpdate(PlayerStatesManager player)
    {
        _walkingDirection = player.input.actions["walk"].ReadValue<float>();
        player.input.onActionTriggered += InputStatesSwitch;
       
        switch (stateNumberSwitch)
        {
            case 1: _spriteTransform.localScale = new Vector3(_spriteTransform.localScale.x, _spriteTransform.localScale.y * 2);
                Walking(0, player.playerRigidbody2D.velocity.y); player.SwitchState(player.airState);
                break;

            case 2:
                _spriteTransform.localScale = new Vector3(_spriteTransform.localScale.x, _spriteTransform.localScale.y * 2);
                if (player.playerRigidbody2D.velocity.x != 0)
                {
                    player.SwitchState(player.walkState);
                }
                else 
                { 
                    player.SwitchState(player.idleState);
                }
                break;
        }
    }

    public override void StateFixedUpdate(PlayerStatesManager player)
    {
        player.playerRigidbody2D.velocity = Walking(_walkingDirection, player.playerRigidbody2D.velocity.y);
    }

    private Vector2 Walking(float direction, float speedCoordY)
    {
        return new Vector2(direction * _walkingSpeed, speedCoordY);
    }

    private void InputStatesSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.action.name)
            {
                case "jump": stateNumberSwitch = 1; break;
            }
        }

        if ((context.canceled) && (context.action.name=="crouch"))
        {
            stateNumberSwitch = 2;
        }
    }
}
