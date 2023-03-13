using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAirState : PlayerBaseState
{
    [SerializeField] private float _jumpForceImpulse;
    [SerializeField] private float _jumpForceHold;
    [SerializeField] private float _timeForJump;
    [SerializeField] private float _gravityForceFall=9;

    [SerializeField] private float _airHorisontalMovementSpeed;
    private float _jumpTimer;
    private bool _jumping = false;
    public override void StateStart(PlayerStatesManager player)
    {
        stateNumberSwitch = 0;

        Jump(player.playerRigidbody2D);
    }

    public override void StateUpdate(PlayerStatesManager player)
    {
        player.input.onActionTriggered += JumpButtonRelease;
    }

    public override void StateFixedUpdate(PlayerStatesManager player)
    {
        player.playerRigidbody2D.velocity = AirMovement(player.input.actions["walk"].ReadValue<float>(),player.playerRigidbody2D);

        if ((Time.time - _jumpTimer >= _timeForJump))
        {
            _jumping = false;
        }

        if (_jumping)
        {
            player.playerRigidbody2D.AddForce(Vector2.up * _jumpForceHold, ForceMode2D.Force);
        }

        else
        {
            player.playerRigidbody2D.gravityScale = _gravityForceFall;
        }
    }

    private void Jump(Rigidbody2D rigidbody2D)
    {
        rigidbody2D.AddForce(Vector2.up * _jumpForceImpulse, ForceMode2D.Impulse);

        _jumpTimer = Time.time;

        _jumping = true;
    }

    private void JumpButtonRelease(InputAction.CallbackContext context)
    {
        if (context.action.name == "jump")
        {
            if (context.canceled)
            {
                _jumping = false;
            }
        }
    }

    private Vector2 AirMovement(float direction, Rigidbody2D rigidbody2D)
    {
        return new Vector2(direction * _airHorisontalMovementSpeed, rigidbody2D.velocity.y);
    }
}
