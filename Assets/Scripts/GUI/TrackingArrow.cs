using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingArrow : MonoBehaviour
{

    [Header("TrackingArrow Components")]
    [SerializeField] SpriteRenderer arrow;
    [SerializeField] SpriteRenderer tracker;
    [SerializeField] Transform rotator;
    public Transform target;
    public float distance = 15;
    Camera cam;

    private void Awake()
    {
        cam = GameCamera.Instance.Cam;
    }

    private void LateUpdate()
    {
        Vector2 targetPosition;

        arrow.enabled = Input.GetKey(KeyCode.T);
        tracker.enabled = Input.GetKey(KeyCode.T);

        if (IsOnScreen(target.position))
        {
            arrow.gameObject.SetActive(false);
            targetPosition = target.position;
        }
        else
        {
            arrow.gameObject.SetActive(true);
            float angleDeg = Vector2.SignedAngle(Vector2.right, (target.position - transform.position).normalized);
            Vector2 screenCenter = cam.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
            targetPosition = screenCenter + new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad)) * distance;

            rotator.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
        }
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }

    public void SetColor(Color color)
    {
        tracker.color = color;
        arrow.color = color;
    }

    bool IsOnScreen(Vector2 worldPoint)
    {
        return cam.pixelRect.Contains(cam.WorldToScreenPoint(worldPoint));
    }

}
