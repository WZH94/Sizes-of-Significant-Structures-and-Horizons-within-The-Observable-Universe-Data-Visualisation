using UnityEngine;

using TMPro;

public class ScaleIndicator : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI indicatorText;
  [SerializeField] private RectTransform scaleBar;

  private const float REFERENCE_WIDTH = 1920f;

  private Camera mainCamera;

  private void Awake()
  {
    mainCamera = Camera.main;
  }

  private void LateUpdate()
  {
    UpdateScale();
  }

  private void UpdateScale()
  {
    Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.2f, 0f, transform.position.y));
    Debug.DrawLine(mainCamera.transform.position, mainCamera.ViewportToWorldPoint(new Vector3(0.2f, 0.0f, transform.position.y)), Color.red);
    Debug.Log(mainCamera.ViewportToWorldPoint(new Vector3(0.2f, 0.0f, transform.position.y)));

    Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

    if (Physics.Raycast(ray, out RaycastHit hit))
    {
      float scale = -hit.point.x * 2f * 1000000f;

      indicatorText.text = scale.ToString("N0") + " Light Years";
    }
  }
}
