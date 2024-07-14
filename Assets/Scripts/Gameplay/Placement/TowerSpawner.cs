using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : MonoBehaviour, IUpdatable
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayermask;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private int enemiesToSpawnIncrement;
    [SerializeField] private float minTapMovement = 50f;

    private Vector3 lastPosition;
    private GameObject currentTower;
    private bool isPlacing = false;
    private Vector3 touchStartPosition;
    public bool IsPlacing => isPlacing;
    
    public List<TowerController> towers = new List<TowerController>();

    public void OnUpdate()
    {
        if (isPlacing && currentTower != null)
        {
            Vector3 position = GetSelectedMapPosition();
            currentTower.transform.position = position;
            SnapToGrid();
        }
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }

    private void SnapToGrid()
    {
        Vector3 position = currentTower.transform.position;
        position.x = Mathf.Round(position.x);
        position.z = Mathf.Round(position.z);
        currentTower.transform.position = position;
    }

    public void StartPlacingTower(BaseEventData data)
    {
        if (GameController.Instance.CurrentGameState != GameState.Placement) return;

        if (!isPlacing)
        {
            touchStartPosition = Input.mousePosition;
            currentTower = Instantiate(towerPrefab);
            isPlacing = true;
        }
    }

    private void OnTowerDestroyed(HealthHolder h)
    {
        towers.Remove(h.gameObject.GetComponent<TowerController>());
    }

    public void StopPlacingTower(BaseEventData data)
    {
        isPlacing = false;
        if (GameController.Instance.CurrentGameState != GameState.Placement) return;
        if (currentTower == null) return;

        if ((Input.mousePosition - touchStartPosition).magnitude < minTapMovement)
        {
            Destroy(currentTower);
            return;
        }

        var data1 = Physics.OverlapSphere(currentTower.transform.position, 0.5f);
        if (data1.Count(x => x.gameObject.layer == (int)Layers.Tower || x.gameObject.layer == (int)Layers.Mines) > 1)
        {
            Destroy(currentTower);
            return;
        }

        var controller = currentTower.GetComponent<TowerController>();
        if (controller != null)
        {
            controller.Init(bulletPool);
            towers.Add(controller);
            currentTower.GetComponent<HealthHolder>().DestroyedAction += OnTowerDestroyed;
        }
        
        var spawners = GameController.Instance.GetEnemySpawners();
        int index = Random.Range(0, spawners.Count);
        spawners[index].enemiesToSpawn += enemiesToSpawnIncrement;
        spawners[index].allEnemiesSpawned = false;
    }
}
