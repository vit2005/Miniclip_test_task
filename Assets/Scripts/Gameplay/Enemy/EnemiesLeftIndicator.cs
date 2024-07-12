using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesLeftIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private List<EnemySpawner> _enemySpawners;

    void Start()
    {
        _enemySpawners = GameController.Instance.GetEnemySpawners();
    }

    void Update()
    {
        int all = 0;
        foreach (var s in _enemySpawners)
        {
            all += s.enemiesToSpawn;
        }

        text.text = all.ToString();
    }
}
