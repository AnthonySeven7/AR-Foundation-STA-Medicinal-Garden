using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that controls all things related to the plants in the application
/// </summary>
public class PlantManager : MonoBehaviour
{
    #region VARIABLES
    public Plant[] plantList; // A list of all plants in the database
    public Plant currentPlant; // The plant that is currently being focused on
    // Start is called before the first frame update
    #endregion //VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS
    
    #endregion //UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    public void Start()
    {
        
    }

    public void onFocusSwitch(Plant focus)
    {
        // currentPlant = focus;
        // Instantiate("moleculeModels/"+currentPlant.molecules[0].name)
    }
    #endregion //PUBLIC_METHODS
}
