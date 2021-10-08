using UnityEngine;

public class CameraController : MonoBehaviour
{
  private float exponentialBase = 100000f;

  public void UpdateCameraZoom( float value )
  {
    Vector3 newPos = transform.position;
    newPos.y = Mathf.Pow(exponentialBase, value);
    transform.position = newPos;
  }
}
