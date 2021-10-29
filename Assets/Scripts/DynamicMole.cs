using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that changes the mesh of the object attached to it. Mesh chosen based on currently selected molecule
/// </summary>
public class DynamicMole : MonoBehaviour
{
    #region VARIABLES
    private GameObject plantManager;
    #endregion //VARIABLES
    // Update is called once per frame
    public void Start() {
        plantManager = GameObject.Find("PlantManager");
        Debug.Log(plantManager.name);
    }
    void Update()
    {
        
    }
}
