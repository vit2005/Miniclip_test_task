using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed; // швидкість панорамування
    public float zoomSpeed = 0.5f; // швидкість масштабування
    public float minZoom; // мінімальне значення масштабування
    public float maxZoom; // максимальне значення масштабування

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (GameController.Instance.GetTowerSpawners().Any(x => x.IsPlacing)) return;
        // Панорамування (переміщення камери вліво-вправо, вгору-вниз)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                // Переміщення камери по осям X і Y на основі руху пальця
                cam.transform.Translate(-touchDeltaPosition.x * panSpeed, -touchDeltaPosition.y * panSpeed, 0);
            }
        }
        // Масштабування (зміна масштабу камери)
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Зміна FOV камери на основі руху пальців
            float newFOV = cam.fieldOfView + deltaMagnitudeDiff * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(newFOV, minZoom, maxZoom);
        }
    }
}
