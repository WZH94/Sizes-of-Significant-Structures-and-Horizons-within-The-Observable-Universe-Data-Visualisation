using UnityEngine;
using UnityEngine.UI;

public class ScaleSlider : MonoBehaviour
{
  [SerializeField] private CameraController cameraController;
  [SerializeField] private RectTransform handle, fill;
  [SerializeField] private RectTransform speedOne, speedTwo, negativeSpeedOne, negativeSpeedTwo;
  [SerializeField] private float normalSpeed = 0.01f, fastSpeed = 0.03f;

  private Slider slider;

  private float currentValue;
  private int currentSpeedSetting = 0;
  private float currentSpeed = 0f;

  private void Awake()
  {
    slider = GetComponent<Slider>();
    currentValue = slider.value;

    currentSpeedSetting = 0;
    AdjustSpeed();
  }

  private void Update()
  {
    if (currentSpeedSetting != 0)
    {
      AutoMoveSlider();
    }
  }

  private void LateUpdate()
  {
    if (currentValue != slider.value)
    {
      LerpValue();
    }
  }

  private void AutoMoveSlider()
  {
    slider.value -= currentSpeed * Time.deltaTime;

    if (slider.value <= slider.minValue || slider.value >= slider.maxValue)
    {
      slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
      currentSpeedSetting = 0;
      AdjustSpeed();
    }
  }

  private void LerpValue()
  {
    currentValue = Mathf.Abs(currentValue - slider.value) <= 0.00001f ? slider.value : Mathf.SmoothStep(currentValue, slider.value, 7f * Time.deltaTime);
    UpdateHandlePosition();

    cameraController.UpdateCameraZoom(currentValue);
  }

  private void UpdateHandlePosition()
  {
    Vector2 anchorMin = new Vector2();
    Vector2 anchorMax = new Vector2(0, 1f);
    anchorMax.x = anchorMin.x = 1f - Mathf.Lerp(0, 1f, (currentValue - slider.minValue) / (slider.maxValue - slider.minValue));

    handle.anchorMax = anchorMax;
    handle.anchorMin = anchorMin;

    fill.anchorMin = anchorMin;
  }

  public void SliderValueChanged()
  {
    UpdateHandlePosition();
  }

  public void SpeedUp()
  {
    if (currentSpeedSetting < 2)
    {
      ++currentSpeedSetting;
      AdjustSpeed();
    }
  }

  public void SpeedDown()
  {
    if (currentSpeedSetting > -2)
    {
      --currentSpeedSetting;
      AdjustSpeed();
    }
  }

  private void AdjustSpeed()
  {
    speedOne.gameObject.SetActive(false);
    speedTwo.gameObject.SetActive(false);
    negativeSpeedOne.gameObject.SetActive(false);
    negativeSpeedTwo.gameObject.SetActive(false);

    switch (currentSpeedSetting)
    {
      case -2:
        currentSpeed = -fastSpeed;

        negativeSpeedOne.gameObject.SetActive(true);
        negativeSpeedTwo.gameObject.SetActive(true);
        break;

      case -1:
        currentSpeed = -normalSpeed;

        negativeSpeedOne.gameObject.SetActive(true);
        break;

      case 0:
        currentSpeed = 0f;

        break;

      case 1:
        currentSpeed = normalSpeed;

        speedOne.gameObject.SetActive(true);
        break;

      case 2:
        currentSpeed = fastSpeed;

        speedOne.gameObject.SetActive(true);
        speedTwo.gameObject.SetActive(true);
        break;

      default:
        Debug.LogError("Invalid setting!" + " " + currentSpeedSetting);
        break;
    }
  }
}
