using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAirState : PlayerBaseState
{
    [SerializeField] private float _jumpForceImpulse=15f;
    [SerializeField] private float _jumpForceHold=3;
    [SerializeField] private float _timeForJump=0.3f;
    [SerializeField] private float _gravityForceFall = 9;

    [SerializeField] private float _airHorisontalMovementSpeed=12f;
    private float _jumpTimer;
    private bool _jumping = false;
    public override void StateStart(PlayerStatesManager player)
    {
        stateNumberSwitch = 0;

        jump(player.playerRigidbody2D);

       
    }
    public override void StateUpdate(PlayerStatesManager player)
    {
        player.input.onActionTriggered += jumpButtonRelease;
    }
    public override void StateFixedUpdate(PlayerStatesManager player)
    {

        player.playerRigidbody2D.velocity = airMovement(player.input.actions["walk"].ReadValue<float>(),player.playerRigidbody2D);

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
    private void jump(Rigidbody2D _rigidbody2D)
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForceImpulse, ForceMode2D.Impulse);

        _jumpTimer = Time.time;

        _jumping = true;
    }
    private void jumpButtonRelease(InputAction.CallbackContext context)
    {
        if (context.action.name == "jump")
        {
            if (context.canceled)
            {
                _jumping = false;
            }
        }

    }
    private Vector2 airMovement(float _direction, Rigidbody2D _rigidbody2D)
    {

        return new Vector2(_direction * _airHorisontalMovementSpeed, _rigidbody2D.velocity.y);

    }
}
