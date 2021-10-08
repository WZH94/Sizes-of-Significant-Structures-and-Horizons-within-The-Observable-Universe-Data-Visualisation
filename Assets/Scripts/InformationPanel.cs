using UnityEngine;

using TMPro;

public class InformationPanel : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI nameDisplay, sizeDisplay, informationDisplay;

  public void SetValues(string name, float size, string information)
  {
    nameDisplay.text = name;
    sizeDisplay.text = size.ToString("N0");
    informationDisplay.text = information;

    gameObject.SetActive(true);
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }
}
