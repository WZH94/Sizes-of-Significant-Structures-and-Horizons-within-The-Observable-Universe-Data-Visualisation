using UnityEngine;

public class Data : MonoBehaviour
{
  public string objectName;
  public float size;
  public string information;

  private float minGlossiness = 0.25f;
  private float maxGlossiness = 0.7f;
  private float glossinessModifier;

  private Renderer renderer;

  private void Awake()
  {
    renderer = GetComponent<Renderer>();

    renderer.material.SetFloat("_Glossiness", Random.Range(minGlossiness, maxGlossiness));
    float glossiness = renderer.material.GetFloat("_Glossiness");

    glossinessModifier = Random.Range(0.02f, 0.05f);

    if (glossiness >= maxGlossiness)
    {
      glossinessModifier *= -1f;
    }
  }

  private void LateUpdate()
  {
    float glossiness = GetComponent<Renderer>().material.GetFloat("_Glossiness");

    glossiness += glossinessModifier * Time.deltaTime;
    GetComponent<Renderer>().material.SetFloat("_Glossiness", glossiness);

    if (glossiness <= minGlossiness || glossiness >= maxGlossiness)
    {
      glossinessModifier *= -1f;
    }
  }
}
