using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State startingState;

    private State currentState;
    private List<State> states;
    private Enemy owner;

    private void Start()
    {
        InitStates();
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void SwitchState(Type newState)
    {
        currentState = states.Where(state => state.GetType() == newState).First();
    }

    private void InitStates()
    {
        owner = GetComponent<Enemy>();
        states = new List<State>(owner.GetComponents<State>());

        foreach (State state in states)
        {
            state.SetUp(owner, this);
        }

        currentState = startingState;
    }
}
