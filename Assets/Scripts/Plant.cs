using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

///<summary>
/// A type for each plant in the database.
/// Usage: Hold all of the information of the plant
///</summary>
public class Plant : MonoBehaviour
{
    #region VARIABLES
    private string comName = string.Empty; // The common name of the plant
    private string sciName = string.Empty; // The scientific name of the plant
    private string family = string.Empty; // The family of the plant
    [Multiline]
    private string description; // The description of the plant
    [SerializeField]
    private Molecule[] molecules; // A list of all the molecules of the plant
    private string toxicity; // The toxicity of the plant
    private string hardiness; // The hardiness zone(s) of the plant
    private string cont; // The contraindications of the plant
    [SerializeField]
    private string links; // A list of extra links associated with the plant
    public static List<List<string>> matrix = new List<List<string>>(); //2D List

    #endregion //VARIABLES

    #region UNITY_MONOBEHAVIOUR_MEDTHODS
    void Start() {
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("<b>{0}</b>\n<i>{1}</i>\n{2}\n{3}", comName, sciName, family, description); // Set the text displayed on the button
        gameObject.name = string.Format("{0}_button",comName.Replace(" ", "")); // Set the name of the game object
        Debug.Log(sciName.Replace(" ","").Replace(".",""));
        if (Resources.Load<Sprite>("Button_Images/"+sciName.Replace(" ", "").Replace(".", "")))
            transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button_Images/"+sciName.Replace(" ", "").Replace(".", ""));
        
        /// Ensure the second word in the scientific name of the plant is lowercase
    }
    #endregion //UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    public void onClick() {
        GameObject.Find("PlantManager").gameObject.GetComponent<PlantManager>().onFocusSwitch(this);
    }
    #region GETTERS
    public string getComName() {
        return comName;
    }
    public string getSciName() {
        return sciName;
    }
    public string getFamily() {
        return family;
    }
    public string getDesc() {
        return description;
    }
    public Molecule[] getMolecules() {
        return molecules;
    }
    public string getToxicity() {
        return toxicity;
    }
    public string getHardiness() {
        return hardiness;
    }

    public string getCont() {
        return cont;
    }
    public string getLinks() {
        return links;
    }
    #endregion //GETTERS

    #region SETTERS
    public void setComName(string x) {
        comName = x;
    }
    public void setSciName(string x) {
        sciName = x;
    }
    public void setFamily(string x) {
        family = x;
    }
    public void setDesc(string x) {
       description = x;
    }
    public void setMolecules(Molecule[] x) {
        molecules = x;
    }
    public void setToxicity(string x) {
        toxicity = x;
    }
    public void setHardiness(string x) {
        hardiness = x;
    }

    public void setCont(string x) {
        cont = x;
    }
    public void setLinks(string x) {
        links = x;
    }
    #endregion //SETTERS
    #endregion //PUBLIC_METHODS
}

#region DISPLAY_EDITOR
#if UNITY_EDITOR

[CustomEditor(typeof(Plant))]
[CanEditMultipleObjects]
public class PlantEditor : Editor {
    public override void OnInspectorGUI() {
        Plant model = (Plant)target;
        if (model.getComName() != "") GUILayout.Label("Common Name: " + model.getComName());
        if (model.getSciName() != "") GUILayout.Label("Scientific Name: " + model.getSciName());
        if (model.getFamily() != "") GUILayout.Label("Family: " + model.getFamily());
        if (model.getDesc() != "") GUILayout.Label("Description: " + model.getDesc());
        if (model.getMolecules().Length != 0) {
            foreach(Molecule mole in model.getMolecules()) {
                if (mole != null) GUILayout.Label("Molecule: " + mole._name);
            }
        }
        if (string.IsNullOrEmpty(model.getToxicity())) GUILayout.Label("Toxicity: " + model.getToxicity());
        if (model.getHardiness() != "") GUILayout.Label("Hardiness: " + model.getHardiness());
        if (model.getLinks().Length != 0) EditorGUILayout.PropertyField(serializedObject.FindProperty("links"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion //DISPLAY_EDITOR
