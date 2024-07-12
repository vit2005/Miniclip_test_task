using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : MonoBehaviour
{

    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayermask;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private BulletPool bulletPool;

    private Vector3 lastPosition;
    private GameObject currentTower;
    private bool isPlacing = false;
    
    private List<TowerController> towers = new List<TowerController>();

    void Update()
    {
        if (isPlacing && currentTower != null)
        {
            Vector3 position = GetSelectedMapPosition();
            currentTower.transform.position = position;
            SnapToGrid();
        }

        foreach (TowerController t in towers)
        {
            t.OnUpdate();
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

    void SnapToGrid()
    {
        Vector3 position = currentTower.transform.position;
        position.x = Mathf.Round(position.x);
        position.z = Mathf.Round(position.z);
        currentTower.transform.position = position;
    }

    public void StartPlacingTower(BaseEventData data)
    {
        if (!isPlacing)
        {
            currentTower = Instantiate(towerPrefab);
            var controller = currentTower.GetComponent<TowerController>();
            controller.Init(bulletPool);
            currentTower.GetComponent<HealthHolder>().DestroyedAction += OnTowerDestroyed;
            towers.Add(controller);
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
    }
}
