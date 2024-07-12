using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed; // �������� �������������
    public float zoomSpeed = 0.5f; // �������� �������������
    public float minZoom; // �������� �������� �������������
    public float maxZoom; // ����������� �������� �������������

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (GameController.Instance.GetTowerSpawners().Any(x => x.IsPlacing)) return;
        // ������������� (���������� ������ ����-������, �����-����)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                // ���������� ������ �� ���� X � Y �� ����� ���� ������
                cam.transform.Translate(-touchDeltaPosition.x * panSpeed, -touchDeltaPosition.y * panSpeed, 0);
            }
        }
        // ������������� (���� �������� ������)
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ���� FOV ������ �� ����� ���� �������
            float newFOV = cam.fieldOfView + deltaMagnitudeDiff * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(newFOV, minZoom, maxZoom);
        }
    }
}
