using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EEnemyState {Idle, Wandering, Chasing}
    EEnemyState currentState = EEnemyState.Idle;
    public EEnemyState CurrentState => currentState;
    NavMeshAgent agent;
    [SerializeField] List<Transform> wanderPositions = new List<Transform>();
    int currentWanderPoint = 0; 
   

    [HideInInspector] public Player player;
    PlayerDetection detection;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        detection = GetComponent<PlayerDetection>();
        detection.Init(this);
        ChangeState(EEnemyState.Wandering);
    }

    void Update()
    {
        switch (currentState)
        {
            case EEnemyState.Idle:
                OnIdleUpdate();
                break;
            case EEnemyState.Wandering:
                OnWanderUpdate();
                break;
            case EEnemyState.Chasing:
                OnChaseUpdate();
                break;
        }
    }

    public void ChangeState(EEnemyState newState)
    {
        if(currentState == newState)
            return;

        currentState = newState;

        switch (currentState)
        {
            case EEnemyState.Idle:
                OnIdleStart();
                break;
            case EEnemyState.Wandering:
                OnWanderStart();
                break;
            case EEnemyState.Chasing:
                OnChaseStart();
                break;
        }
    }

    // ----- IDLE STATE ------------
    void OnIdleStart()
    {
        agent.isStopped = true;
    }
    void OnIdleUpdate()  {    }

    // ----- WANDER STATE ------------
    void OnWanderStart()
    {
        agent.isStopped = false;
        agent.SetDestination(wanderPositions[currentWanderPoint].position);
    }
    void OnWanderUpdate()
    {
        detection.CheckDistanceFromPlayer();
        if(!agent.hasPath)
        {
            currentWanderPoint = (currentWanderPoint + 1) % wanderPositions.Count;
            agent.SetDestination(wanderPositions[currentWanderPoint].position);
        }             
    }
    // ----- CHASE STATE ------------
    void OnChaseStart()
    {
        agent.isStopped = false;
    }
    void OnChaseUpdate()
    {
        agent.SetDestination(player.transform.position);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            gameManager.EndGame();
        }
    }



}
