using UnityEngine;

public abstract class GameStateBase : IState
{
    protected GameManager _gameManager;

    public GameStateBase(GameManager gameManager)
    {
        this._gameManager = gameManager;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
}

public enum EGameState
{
    NotSet,
    Menu,
    Play
}