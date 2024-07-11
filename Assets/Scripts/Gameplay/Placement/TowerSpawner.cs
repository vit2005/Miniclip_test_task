using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : MonoBehaviour
{

    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayermask;
    [SerializeField] private GameObject debugGameObject;
    private Vector3 lastPosition;

    public GameObject towerPrefab;
    private GameObject currentTower;
    private bool isPlacing = false;

    void Update()
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
            isPlacing = true;
        }
    }

    public void StopPlacingTower(BaseEventData data)
    {
        isPlacing = false;
    }
}
