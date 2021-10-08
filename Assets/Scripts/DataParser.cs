using System;
using System.Collections.Generic;
using UnityEngine;

public class DataParser : MonoBehaviour
{
  // Name of the input file, no extension
  [SerializeField] private string inputfile;
  [SerializeField] private Material[] dataMaterials;
  [SerializeField] private GameObject universeParent;

  // List for holding data from CSV reader
  private List<Dictionary<string, object>> pointList;

  // Indices for columns to be assigned
  public int columnX = 0;
  public int columnY = 1;
  public int columnZ = 2;

  // Full column names
  public string xName;
  public string yName;
  public string zName;

  public float plotScale = 10;

  // Use this for initialization
  void Start()
  {
    Debug.Log(Mathf.Exp(5));
    // Set pointlist to results of function Reader with argument inputfile
    pointList = CSVReader.Read(inputfile);

    //Log to console
    Debug.Log(pointList);

    if (pointList.Count != dataMaterials.Length) Debug.LogError("Material count does not match number of data!");

    // Declare list of strings, fill with keys (column names)
    List<string> columnList = new List<string>(pointList[1].Keys);

    Debug.Log("====================================== CSV DATA ======================================");

    // Print number of keys (using .count)
    Debug.Log("There are " + columnList.Count + " columns in CSV");

    foreach (string key in columnList)
      Debug.Log("Column name is " + key);

    // Assign column name from columnList to Name variables
    xName = columnList[columnX];
    yName = columnList[columnY];
    zName = columnList[columnZ];

    float cylinderY = 0f;
    float incrementY = 0.1f;

    //Loop through Pointlist
    for (var i = 0; i < pointList.Count; i++)
    {
      // Get value in poinList at ith "row", in "column" Name
      string name = Convert.ToString(pointList[i][xName]);
      float size = Convert.ToSingle(pointList[i][yName]);
      string information = Convert.ToString(pointList[i][zName]);

      GameObject celestialObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
      celestialObject.name = name;
      celestialObject.transform.position = Vector3.zero + Vector3.up * cylinderY;
      celestialObject.transform.localScale = new Vector3(size, 1f, size);

      celestialObject.transform.parent = universeParent.transform;

      celestialObject.GetComponent<Renderer>().material = dataMaterials[i];
      celestialObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      Destroy(celestialObject.GetComponent<CapsuleCollider>());
      celestialObject.AddComponent<MeshCollider>();

      Data data = celestialObject.AddComponent<Data>();
      data.objectName = name;
      data.size = size;
      data.information = information;

      celestialObject.isStatic = true;

      cylinderY += incrementY;
    }
  }
}