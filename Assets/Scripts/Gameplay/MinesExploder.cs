using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MinesExploder : MonoBehaviour
{
    [SerializeField] private float maxTapDuration = 0.5f;
    [SerializeField] private float maxTapMovement = 50f;
    [SerializeField] private int minesDamage = 1;
    [SerializeField] private float minesRadius = 3f;

    private bool isTouching = false;
    private float touchStartTime;
    private Vector2 touchStartPosition;
    

    void Update()
    {
        if (GameController.Instance.CurrentGameState != GameState.Fighting) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    touchStartTime = Time.time;
                    touchStartPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (isTouching &&
                        (Time.time - touchStartTime) < maxTapDuration &&
                        (touch.position - touchStartPosition).magnitude < maxTapMovement)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Mines")))
                        {
                            Explode(hit.transform.position);
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    isTouching = false;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Canceled:
                    isTouching = false;
                    break;
            }
        }
    }

    private void Explode(Vector3 position)
    {
        var colliders = Physics.OverlapSphere(position, minesRadius, LayerMask.GetMask("Enemy"));
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                var healthHolder = collider.gameObject.GetComponent<HealthHolder>();
                healthHolder?.Damage(minesDamage);
            }
        }
    }
}
