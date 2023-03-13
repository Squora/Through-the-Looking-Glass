using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatesManager : MonoBehaviour
{
    [HideInInspector] public PlayerInput input;

    [HideInInspector] public Rigidbody2D playerRigidbody2D;

    [HideInInspector] public PlayerBaseState currentState;

    [HideInInspector] public PlayerIdleState idleState;

    [HideInInspector] public PlayerCrouchState crouchState;

    [HideInInspector] public PlayerWalkState walkState;

    [HideInInspector] public PlayerAirState airState;

    private BoxCollider2D _playerBoxCollider2D;

    [SerializeField] private Vector2 _playerGroundCollisionCheckBox;

    [SerializeField] private float _normalGravityScale;

    [SerializeField] private LayerMask _groundLayer;

    void Start()
    {
        input = GetComponent<PlayerInput>();

        playerRigidbody2D = GetComponent<Rigidbody2D>();

        _playerBoxCollider2D = GetComponentInChildren<BoxCollider2D>();

        idleState = GetComponent<PlayerIdleState>();

        walkState = GetComponent<PlayerWalkState>();

        crouchState = GetComponent<PlayerCrouchState>();

        airState = GetComponent<PlayerAirState>();

        currentState = idleState;

        currentState.StateStart(this);
    }

    void Update()
    {
        currentState.StateUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate(this);
        groundCollisionCheck();
    }

    public void SwitchState(PlayerBaseState state)
    {
        Debug.Log("leaving " + currentState + " state");
        Debug.Log("entering " + state + " state");
        currentState = state;
        state.StateStart(this);
    }

    private void groundCollisionCheck()
    {
        RaycastHit2D groundCheck = Physics2D.BoxCast(transform.position - Vector3.up * _playerBoxCollider2D.size.y / 2, 
            _playerGroundCollisionCheckBox, 0, Vector2.down, 0, _groundLayer);

        Debug.DrawRay(transform.position - Vector3.right * _playerBoxCollider2D.size.x / 2, Vector2.down * _playerGroundCollisionCheckBox.y);
        Debug.DrawRay(transform.position + Vector3.right * _playerBoxCollider2D.size.x / 2, Vector2.down * _playerGroundCollisionCheckBox.y);

        if ((groundCheck.collider == true) && (currentState == airState) && (playerRigidbody2D.velocity.y <= 0))
        {
            if (playerRigidbody2D.velocity.x == 0)
            {
                SwitchState(idleState);
            }
            else
            {
                SwitchState(walkState);
            }
            playerRigidbody2D.gravityScale = _normalGravityScale;
        }

        if ((groundCheck.collider == false) && (currentState != airState))
        {
            currentState = airState;
        }
    }
}
