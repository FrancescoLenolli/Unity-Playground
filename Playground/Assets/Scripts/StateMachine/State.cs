using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    private Enemy owner;
    private StateMachine stateMachine;

    public Enemy Owner { get => owner; }
    public StateMachine StateMachine { get => stateMachine; }

    public void SetUp(Enemy owner, StateMachine stateMachine)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
    }

    public abstract void UpdateState();
}
