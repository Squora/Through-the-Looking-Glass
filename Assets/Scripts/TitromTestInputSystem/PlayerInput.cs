using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInput : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] public float speed;

    private PlayerAction _action;

    private InputAction _move;

    private InputAction _talk;

    private InputAction _nextDialog;

    private Collider2D _npc;

    private void Awake()
    {
        _action = new PlayerAction();

        ConnectInputSystemModule();

        ConnectEvent();

    }

    private void ConnectInputSystemModule()
    {
        _move = _action.Player.Move;

        _talk = _action.Player.Talk;

        _nextDialog = _action.Player.NextDialog;
    }

    private void ConnectEvent()
    {

        FindObjectOfType<DialogSystem>().onMove += () => { _move.Enable(); };

        FindObjectOfType<DialogSystem>().onNextDialog += (typeDialog) =>
        {
            if (typeDialog.Equals(TypeDialog.Message)) _nextDialog.Enable();
            else _nextDialog.Disable();
        };

        _talk.performed += StartTalk;

        _nextDialog.performed += (callback) => { FindObjectOfType<DialogSystem>().DisplayNextSentense(); };
    }

    private void Update()
    {
        transform.Translate(_move.ReadValue<Vector2>() * speed);
    }

    private void StartTalk(CallbackContext callback)
    {

        FindObjectOfType<DialogSystem>().StartDialog(_npc.GetComponent<NPC>().dialogs);
        _move.Disable();
        _talk.Disable();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            _talk.Enable();
            _npc = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            _talk.Disable();
            _nextDialog.Disable();
        }
    }

    private void OnEnable()
    {
        _move.Enable();

    }
    private void OnDisable()
    {
        _move.Disable();
        _talk.Disable();
        _nextDialog.Disable();

    }
}
