using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCrouchState : PlayerBaseState
{
    [SerializeField] private float walkingSpeed;
    private float walkingDirection;
    Vector2 walkingVector;
    private Transform spriteTransform;
    public override void StateStart(PlayerStatesManager player)
    {
        spriteTransform = transform.GetComponentInChildren<Transform>();
        spriteTransform.localScale=new Vector3(spriteTransform.localScale.x,spriteTransform.localScale.y/2);
        transform.position = transform.position - spriteTransform.localScale.y / 2 * Vector3.up;
        stateNumberSwitch = 0;
    }
    public override void StateUpdate(PlayerStatesManager player)
    {
        walkingDirection = player.input.actions["walk"].ReadValue<float>();
        player.input.onActionTriggered += InputStatesSwitch;
       
        switch (stateNumberSwitch)
        {
            case 1: spriteTransform.localScale = new Vector3(spriteTransform.localScale.x, spriteTransform.localScale.y *2); walking(0, player.playerRigidbody2D.velocity.y); player.SwitchState(player.airState); break;

            case 2:
                spriteTransform.localScale = new Vector3(spriteTransform.localScale.x, spriteTransform.localScale.y * 2);
                if (player.playerRigidbody2D.velocity.x != 0) { player.SwitchState(player.walkState); }
                else { player.SwitchState(player.idleState); }
                break;
        }
    }
    public override void StateFixedUpdate(PlayerStatesManager player)
    {
        player.playerRigidbody2D.velocity = walking(walkingDirection, player.playerRigidbody2D.velocity.y);
    }
    private Vector2 walking(float direction, float speedCoordY)
    {
        return new Vector2(direction * walkingSpeed, speedCoordY);
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
        if ((context.canceled)&&(context.action.name=="crouch"))
        {
            stateNumberSwitch = 2;
        }
    }
}
