using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : PersistentMonoSingleton<GameManager>
{
    public IState CurrentState;

    private Dictionary<EGameState, IState> stateLookup = new Dictionary<EGameState, IState>();

    public Button StartButton;
    

    void Start()
    {
        AppEvents.OnGameStateUpdate.OnTrigger += GameStateUpdated;
        StartButton?.onClick.AddListener(StartGame);

        InitializeGameStates();
    }

    void OnDestroy()
    {
        AppEvents.OnGameStateUpdate.OnTrigger -= GameStateUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.OnUpdate();
        if (TrustManager.Instance.PlayerTrust.TotalValue == 0 || CurrencyManager.Instance.Wealth.TotalValue == 0)
        {
            Application.Quit();
        }
    }

    private void InitializeGameStates()
    {
        stateLookup.Add(EGameState.Menu, new MenuState(this));
        stateLookup.Add(EGameState.Play, new PlayState(this));
        CurrentState = stateLookup[EGameState.Menu];
    }

    public void StartGame()
    {
        AppEvents.OnGameStateUpdate.Trigger(EGameState.Play);
    }

    public void GameStateUpdated(EGameState stateId)
    {
        IState newState = stateLookup[stateId];
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}
