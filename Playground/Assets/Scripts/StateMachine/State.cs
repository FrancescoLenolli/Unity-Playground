using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    private Enemy owner;
    private StateMachine stateMachine;

    public Enemy Owner { get => owner; set => owner = value; }
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }

    public abstract void UpdateState();
}
