using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentMonoSingleton<GameManager>
{
    public IState CurrentState;
    

    void Start()
    {
        AppEvents.OnGameStateUpdate += GameStateUpdated;
    }

    void OnDestroy()
    {
        AppEvents.OnGameStateUpdate -= GameStateUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.OnUpdate();
    }

    public void GameStateUpdated(IState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}
