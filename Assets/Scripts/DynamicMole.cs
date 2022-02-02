using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A script that changes the mesh of the object attached to it. Mesh chosen based on currently selected molecule
/// </summary>
public class DynamicMole : MonoBehaviour
{
    #region VARIABLES
    private PlantManager plantManager;
    public bool _flag = false;
    #endregion //VARIABLES
    // Update is called once per frame
    public void Start() {
        GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += "Start\n";
        plantManager = GameObject.Find("PlantManager").GetComponent<PlantManager>();
        if(plantManager.getMole() != null) {
            GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += string.Format("--{0}\n",plantManager.getMole()._name);
        }
        else {
            GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += "NULL\n";
        }
    }

    // Create a trackable molecule for the AR feature
    public void createTrackable(int x) {
        GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += "CREATE_TRACKABLE\n";
        _flag = true;
        // Find the molecule in our folder of models
        //GameObject model;
        // Create a copy of this model and make it a child of this game object
        Molecule mole = plantManager.getPlant().getMolecules()[x];
        Debug.Log(mole);
        GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += string.Format("{0}\n",mole._name);

        if (mole != null) {
            GameObject moleModel = null;
            if (!string.IsNullOrEmpty(mole._name)) {
                moleModel = Resources.Load<GameObject>("Models/" + mole._name);
                if (moleModel == null) Debug.Log(string.Format("<color=red>{0}</color>",mole._name));
                else Debug.Log(string.Format("<color=green>{0}</color>", mole._name));
            }
            if (moleModel != null) {
                GameObject trackedMole = Instantiate(moleModel);
                trackedMole.transform.SetParent(gameObject.transform);
            }
        }
    }

    public void deleteTrackable() {
        Debug.Log("Delete");
        _flag = false;
        foreach (Transform child in transform) { // Destroy the children
            GameObject.Destroy(child.gameObject);
        }
        
    }
}
