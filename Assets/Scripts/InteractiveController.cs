using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractiveController : MonoBehaviour
{
  [SerializeField] private InformationPanel informationPanel;
  [SerializeField] private Slider scaleSlider;

  private Camera mainCamera;
  private GameObject currentMouseOverData;
  private GameObject currentSelectedData;

  private void Awake()
  {
    mainCamera = GetComponent<Camera>();
  }

  private void Update()
  {
    KeyControls();
    ScrollSlider();
    InformationInteraction();
  }

  private void KeyControls()
  {
    if (Input.GetButtonDown("Horizontal"))
    {
      if (Input.GetAxisRaw("Horizontal") > 0)
      {
        scaleSlider.GetComponent<ScaleSlider>().SpeedUp();
      }

      else
      {
        scaleSlider.GetComponent<ScaleSlider>().SpeedDown();
      }
    }
  }

  private void ScrollSlider()
  {
    Vector2 mouseScrollDelta = Input.mouseScrollDelta;

    if (mouseScrollDelta.y != 0f)
    {
      scaleSlider.value += -mouseScrollDelta.y * Time.deltaTime;
    }
  }

  private void InformationInteraction()
  {
    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out RaycastHit hit))
    {
      GameObject mouseOverObject = hit.collider.gameObject;

      if (currentMouseOverData != null && mouseOverObject != currentMouseOverData)
      {
        // Remove hover effect of previous mouse over object
        currentMouseOverData.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      }

      currentMouseOverData = mouseOverObject;
      currentMouseOverData.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

      Data data = hit.collider.GetComponent<Data>();

      if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
      {
        informationPanel.SetValues(data.objectName, data.size, data.information);
      }
    }

    else
    {
      if (currentMouseOverData != null)
      {
        currentMouseOverData.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        currentMouseOverData = null;
      }
    }
  }
}
