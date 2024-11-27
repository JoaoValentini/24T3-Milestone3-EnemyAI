using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    enum State { Idle, Walking, Jumping }

    State currentState;
    void Start()
    {
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
        if(Input.GetKeyDown(KeyCode.W))
            ChangeState(State.Walking);
        else if(Input.GetKeyDown(KeyCode.Space))
            ChangeState(State.Jumping);
        else if(Input.GetKeyDown(KeyCode.S))
            ChangeState(State.Idle);
        
    }

    void ChangeState(State newState)
    {
        currentState = newState;
    }

    void HandleState()
    {
        switch (currentState)
        {
            case State.Idle:
            Debug.Log("Character is Idle");
                break;
            case State.Walking:
            Debug.Log("Character is Walking");
                break;
            case State.Jumping:
            Debug.Log("Character is Jumping");
                break;
        }
    }
}
