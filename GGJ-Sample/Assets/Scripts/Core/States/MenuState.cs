using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : GameStateBase
{
    public EGameState id = EGameState.Menu;

    public MenuState(GameManager gameManager) : base(gameManager) {}

    public override void OnEnter()
    {
        base.OnEnter();

        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
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
