using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayState : GameStateBase
{
    protected EGameState id = EGameState.Play;

    public PlayState(GameManager gameManager) : base(gameManager) {}

    public override void OnEnter()
    {
        base.OnEnter();
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
