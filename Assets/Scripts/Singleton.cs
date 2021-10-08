using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T _instance;
  public static T Instance
  {
    get
    {
      if (_instance == null)
      {
        T[] results = FindObjectsOfType<T>();

        if (results.Length == 0)
        {
          Debug.LogError("Singleton TimeManager has no active instance!");

          return null;
        }

        if (results.Length > 1)
        {
          Debug.LogError("Singleton TimeManager has more than 1 active instance! Number of instances " + results.Length);

          return null;
        }

        _instance = results[0];
      }

      return _instance;
    }
  }
}
