using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementGameState : GameStateBase
{
    public override GameState id => GameState.Placement;

    private List<TowerSpawner> spawners = new List<TowerSpawner>();

    public override void Init()
    {
        spawners = GameController.Instance.GetTowerSpawners();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        foreach (var item in spawners)
        {
            item.OnUpdate();
        }
    }

}
