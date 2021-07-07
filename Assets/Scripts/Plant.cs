using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public string comName = "Test Name"; // The common name of the plant
    public string sciName = "Test Scientific"; // The scientific name of the plant
    public string family = "Test Family"; // The family of the plant
    [Multiline]
    public string description = "This is a quick example of a test description for a plant. <color=red>You</color> <size=15>could</size> also control the characteristics of the text with <b>'text rich'</b> formatting."; // The description of the plant
    public Molecule[] molecules; // A list of all the molecules of the plant
    public string toxicity; // The toxicity of the plant
    public string hardiness; // The hardiness zone(s) of the plant
    public string[] links; // A list of extra links associated with the plant

    void Start() {
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("<b>{0}</b>\n<i>{1}</i>\n{2}\n{3}", comName, sciName, family, description); // Set the text displayed on the button
        gameObject.name = string.Format("{0}_button",comName.Replace(" ", "")); // Set the name of the game object
        /// Ensure the secondd word in the scientific name of the plant is lowercase
    }
    public void onClick() {
        // PlantManager.GetComponent<PlantManager>().onFocusUpdate(this);
    }
}

#region DISPLAY_EDITOR
#if UNITY_EDITOR

[CustomEditor(typeof(Plant))]
[CanEditMultipleObjects]
public class PlantEditor : Editor {
    public override void OnInspectorGUI() {
        Plant model = (Plant)target;
        if (model.comName != "") GUILayout.Label("Common Name: " + model.comName);
        if (model.sciName != "") GUILayout.Label("Scientific Name: " + model.sciName);
        if (model.family != "") GUILayout.Label("Family: " + model.family);
        if (model.description != "") GUILayout.Label("Description: " + model.description);
        if (model.molecules.Length != 0) EditorGUILayout.PropertyField(serializedObject.FindProperty("molecules"), true);
        if (model.toxicity != "") GUILayout.Label("Toxicity: " + model.toxicity);
        if (model.hardiness != "") GUILayout.Label("Hardiness: " + model.hardiness);
        if (model.links.Length != 0) EditorGUILayout.PropertyField(serializedObject.FindProperty("links"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion //DISPLAY_EDITOR
