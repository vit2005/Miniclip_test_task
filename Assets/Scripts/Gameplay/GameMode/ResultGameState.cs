using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGameState : GameStateBase
{
    public override GameState id => GameState.Result;

    public override void Init()
    {
        base.Init();
        GameController.Instance.VictoryScreen.SetActive(false);
        GameController.Instance.DefeatScreen.SetActive(false);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (GameController.Instance.GameResult) GameController.Instance.VictoryScreen.SetActive(true);
        else GameController.Instance.DefeatScreen.SetActive(true);
    }
}
