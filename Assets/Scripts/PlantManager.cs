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
    [SerializeField]
    private GameObject buttonPrefab;
    public List<List<string>> matrix = new List<List<string>>(); //2D List
    public List<Plant> myPlants = new List<Plant>(); //create a list of plants equal to number of rows in matrix
    [SerializeField]
    private GameObject informationPanel;
    [SerializeField]
    private TextMeshProUGUI information;
    [SerializeField]
    private Plant currentPlant; // The plant that is currently being focused on
    [SerializeField]
    private Molecule currentMole; // The molecule that is currently being tracked

    #endregion //VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    #endregion //UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    public void Start()
    {
        matrix = GetComponent<FileReader>().readFile();
        createPlants();
    }


    void createPlants()
    {

        //get column numbers for respective data
        int colComName = getColumnNum("Common Name");
        int colDescription = getColumnNum("Plant Description");
        int colFamily = getColumnNum("Family");
        int colHardiness = getColumnNum("Hardiness");
        int colSciName = getColumnNum("Latin Name");
        int colToxicity = getColumnNum("Toxicity");

        int plantNum = 1; //plant/row number
        Debug.Log(colComName);
        Debug.Log(colDescription);
        Debug.Log(colFamily);
        Debug.Log(colHardiness);
        Debug.Log(colSciName);
        Debug.Log(colToxicity);
        for (int i = 1; i < matrix.Count; i++)
        {
            Debug.Log("creating buttons");
            var button = Instantiate(buttonPrefab).GetComponent<Plant>();
            button.setComName(matrix[plantNum][colComName]);
            button.setDesc(matrix[plantNum][colDescription]);
            button.setFamily(matrix[plantNum][colFamily]);
            button.setHardiness(matrix[plantNum][colHardiness]);
            button.setSciName(matrix[plantNum][colSciName]);
            button.setToxicity(matrix[plantNum][colToxicity]);
            button.transform.parent = GameObject.Find("MainPanel").transform.GetChild(2).GetChild(0).GetChild(0);
            button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            button.gameObject.name = string.Format("{0}_button", button.getComName().Replace(" ", ""));
            myPlants.Add(button);

            /*
            plant.setComName(matrix[plantNum][colComName]);
            plant.setDesc(matrix[plantNum][colDescription]);
            plant.setFamily(matrix[plantNum][colFamily]);
            plant.setHardiness(matrix[plantNum][colHardiness]);
            plant.setSciName(matrix[plantNum][colSciName]);
            plant.setToxicity(matrix[plantNum][colToxicity]);
            */
            plantNum++;
        }
    }

    /*Brief: gets column numbers for respective data
     * @param: to check column(index) for
     * @return: returns the correct column number(index) that contains the string @param
    */
    int getColumnNum(string str)
    {
        //Debug.Log(matrix[0].Count);
        for (int column = 0; column < matrix[0].Count; column++)
        {
            //Debug.Log(column);
            if (matrix[0][column].ToLower().Contains(str)) return column;
        }
        return matrix[0].FindIndex(x => x.StartsWith(str));
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
        if (mole.Length != 0)
        {
            information.text += "<b>Molecules</b>\n";
            foreach (Molecule child in mole)
            {
                string mole_desc = child.getDesc();
                information.text += "<b>" + child.getName() + "</b>\n\n";
                information.text += child.getClass() + "\n\n";
                if (mole_desc != "") information.text += mole_desc + "\n\n";
                information.text += "\n\n";
            }
        }
        if (hardiness != "") information.text += "Hardiness Zone " + hardiness + "\n\n";
        if (toxicity != "") information.text += "Toxicity " + toxicity + "\n\n";
        if (links.Length != 0)
        {
            //Create an instance of a clickable hyperlink
        }
        // Instantiate("moleculeModels/"+currentPlant.molecules[0].name)
    }
    #endregion //PUBLIC_METHODS
}
