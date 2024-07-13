using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightingGameState : GameStateBase
{
    public override GameState id => GameState.Fighting;

    private List<EnemySpawner> spawners;
    private List<TowerController> towers;

    public override void Init()
    {
        spawners = GameController.Instance.GetEnemySpawners();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        towers = new List<TowerController>();
        var towerSpawners = GameController.Instance.GetTowerSpawners();
        foreach (var towerSpawner in towerSpawners)
        {
            foreach (var tower in towerSpawner.towers)
            {
                towers.Add(tower);
                var healthHolder = tower.GetComponent<HealthHolder>();
                healthHolder.DestroyedAction += OnTowerDestroyed;
            }
        }

        foreach (var s in spawners)
        {
            foreach (var e in s.enemies)
            {
                e.StartMoving();
            }
        }
    }

    private void OnTowerDestroyed(HealthHolder healthHolder)
    {
        towers.Remove(healthHolder.GetComponent<TowerController>());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        foreach (var item in spawners)
        {
            item.OnUpdate();
        }
        foreach (var item in towers)
        {
            item.OnUpdate();
        }

        if (spawners.All(x => x.allEnemiesSpawned))
        {
            bool somebodyAlive = false;
            foreach (var item in spawners)
            {
                if (item.enemies.Any())
                {
                    somebodyAlive = true;
                    break;
                }
            }
            if (somebodyAlive == false) GameController.Instance.AllEnemiesAreDead();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        foreach (var s in spawners)
        {
            foreach (var e in s.enemies)
            {
                e.StopMoving();
            }
        }
    }
}
