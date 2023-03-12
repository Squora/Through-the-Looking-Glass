using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAirState : PlayerBaseState
{
    [SerializeField] private float jumpForceImpulse;
    [SerializeField] private float jumpForceHold;
    [SerializeField] private float timeForJump;
    [SerializeField] private float gravityForceFall=9;

    [SerializeField] private float airHorisontalMovementSpeed;
    private float jumpTimer;
    private bool jumping=false;
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

        if ((Time.time - jumpTimer >= timeForJump))
        {
            jumping = false;
        }

        if (jumping)
        {

            player.playerRigidbody2D.AddForce(Vector2.up * jumpForceHold, ForceMode2D.Force);

        }
        else
        {
            player.playerRigidbody2D.gravityScale = gravityForceFall;
        }
      
    }
    private void jump(Rigidbody2D rigidbody2D)
    {
        rigidbody2D.AddForce(Vector2.up * jumpForceImpulse, ForceMode2D.Impulse);

        jumpTimer = Time.time;

        jumping = true;
    }
    private void jumpButtonRelease(InputAction.CallbackContext context)
    {
        if (context.action.name == "jump")
        {
            if (context.canceled)
            {
                jumping = false;
            }
        }

    }
    private Vector2 airMovement(float direction,Rigidbody2D rigidbody2D)
    {

        return new Vector2(direction * airHorisontalMovementSpeed, rigidbody2D.velocity.y);

    }
}
