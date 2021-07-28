using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A script that controls all things related to the plants in the application
/// </summary>
public class PlantManager : MonoBehaviour
{
    #region VARIABLES
    public Plant[] plantList; // A list of all plants in the database
    [SerializeField]
    private GameObject informationPanel;
    [SerializeField]
    private TextMeshProUGUI information;
    private Plant currentPlant; // The plant that is currently being focused on
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
        // Initialize Varialbes
        string desc = focus.getDesc();
        Molecule[] mole = focus.getMolecules();
        string hardiness = focus.getHardiness();
        string toxicity = focus.getToxicity();
        string[] links = focus.getLinks();
        currentPlant = focus;
        informationPanel.SetActive(true);
        // Reset Information Panel and Begin Adding Information To It
        information.text = "<b>" + focus.getComName() + "</b>\n\n";
        information.text += "<i>" + focus.getSciName() + "</i>\n\n";
        if (desc != "") information.text += desc + "\n\n";
        if (mole.Length != 0) {
            information.text += "<b>Molecules</b>\n";
            foreach (Molecule child in mole) {
                string mole_desc = child.getDesc();
                information.text += "<b>" + child.getName() + "</b>\n\n";
                information.text += child.getClass() + "\n\n";
                if (mole_desc != "") information.text += mole_desc + "\n\n";
                information.text += "\n\n";
            }
        }
        if (hardiness != "") information.text += "Hardiness Zone " + hardiness + "\n\n";
        if (toxicity != "") information.text += "Toxicity " + toxicity + "\n\n";
        if (links.Length != 0) {
            //Create an instance of a clickable hyperlink
        }
        // Instantiate("moleculeModels/"+currentPlant.molecules[0].name)
    }
    #endregion //PUBLIC_METHODS
}
