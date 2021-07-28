using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A type for each molecule in the database
/// Usage: Hold all of the information of the molecule
/// </summary>
public class Molecule : MonoBehaviour
{
    #region VARIALBES
    public string _name; // The name of the molecule
    public string _class; // The class of the molecule
    [Multiline]
    public string _description; // The description of the molecule
    #endregion //VARIABLES

    #region PUBLIC_METHODS
    public string getName() {
        return _name;
    }
    public string getClass() {
        return _class;
    }
    public string getDesc(){
        return _description;
    }
    #endregion //PUBLIC_METHODS
}
