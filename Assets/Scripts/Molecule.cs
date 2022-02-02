using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A type for each molecule in the database
/// Usage: Hold all of the information of the molecule
/// </summary>
public class Molecule
{
    #region VARIALBES
    public string _name; // The name of the molecule
    public string _class; // The class of the molecule
    [Multiline]
    public string _description; // The description of the molecule
    #endregion //VARIABLES

    /*#region PUBLIC_METHODS
    #region GETTERS
    public string getName() {
        return _name;
    }
    public string getClass() {
        return _class;
    }
    public string getDesc(){
        return _description;
    }
    #endregion // GETTERS
    #region SETTERS
    public void setName(string x) {
        _name = x;
    }
    public void setClass(string x)
    {
        _class = x;
    }
    public void setDesc(string x)
    {
        _description = x;
    }
    #endregion // SETTERS
    #endregion //PUBLIC_METHODS*/
}