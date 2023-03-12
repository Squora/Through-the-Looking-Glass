using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    [HideInInspector] public int stateNumberSwitch = 0;
    public abstract void StateStart(PlayerStatesManager player);
    public abstract void StateUpdate(PlayerStatesManager player);
    public abstract void StateFixedUpdate(PlayerStatesManager player);
}
