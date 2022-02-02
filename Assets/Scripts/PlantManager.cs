using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    private DynamicMole dynMol; // A reference to the dynamic trackable molecule handler
    private List<GameObject> objToDelete = new List<GameObject>(); // A list of game objects to be deleted once the focus has changed

    #endregion //VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    #endregion //UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    public void Start()
    {
        try {
            dynMol = GameObject.Find("DynamicTrackable").GetComponent<DynamicMole>();
        }
        catch(Exception e){
            print("error");
        }
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
        int[] colMoleName = {getColumnNum("Molecule A"), getColumnNum("Molecule C"), getColumnNum("Molecule B")};
        int colLinks = getColumnNum("Extra Links (Separate by Space)");
        int colCont = getColumnNum("Contraindications");
        int plantNum = 1; //plant/row number
        // Debug.Log(colComName);
        // Debug.Log(colDescription);
        // Debug.Log(colFamily);
        // Debug.Log(colHardiness);
        // Debug.Log(colSciName);
        // Debug.Log(colToxicity);
        for (int i = 1; i < matrix.Count; i++)
        {
            Debug.Log("Creating Button: " + matrix[plantNum][colComName]);
            var button = Instantiate(buttonPrefab).GetComponent<Plant>();
            button.setComName(matrix[plantNum][colComName]);
            button.setDesc(matrix[plantNum][colDescription]);
            button.setFamily(matrix[plantNum][colFamily]);
            button.setHardiness(matrix[plantNum][colHardiness]);
            button.setSciName(matrix[plantNum][colSciName]);
            button.setToxicity(matrix[plantNum][colToxicity]);
            button.setCont(matrix[plantNum][colCont]);
            button.setLinks(matrix[plantNum][colLinks]);
            Molecule[] molecules = new Molecule[3];
            int index = 0;
            foreach (int j in colMoleName) {
                Molecule m = new Molecule();
                if (!string.IsNullOrEmpty(matrix[plantNum][j])) Debug.Log(string.Format("\t{0} {1} {2}",matrix[plantNum][j], matrix[plantNum][j+1], matrix[plantNum][j+2]));
                m._name = matrix[plantNum][j];
                m._class = matrix[plantNum][j+1]; // FIX THIS
                m._description = matrix[plantNum][j+2];
                molecules[index] = m;
                index++;
            }
            button.setMolecules(molecules);
            button.transform.SetParent(GameObject.Find("MainPanel").transform.GetChild(2).GetChild(0).GetChild(0));
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
        for (int column = 0; column < matrix[0].Count; column++)
        {
            if (matrix[0][column].ToLower().Contains(str)) return column;
        }
        return matrix[0].FindIndex(x => x.StartsWith(str));
    }

    public void onFocusSwitch(Plant focus)
    {
        // Initialize Varialbes
        string desc = focus.getDesc();              // description
        Molecule[] mole = focus.getMolecules();     // list of molecules
        string hardiness = focus.getHardiness();    // hardiness zones
        string toxicity = focus.getToxicity();      // toxicity
        string links = focus.getLinks();            // extra links
        string cont = focus.getCont();              // contraindications
        currentPlant = focus;
        informationPanel.SetActive(true);
        // Reset Information Panel and Begin Adding Information To It
        information.text = "<b>" + focus.getComName() + "</b>\n\n";
        information.text += "<i>" + focus.getSciName() + "</i>\n\n";
        if (desc != "") information.text += desc + "\n\n";
        if (mole.Length != 0) {
            information.text += "<b>Molecules</b>\n";
            int i = 0;
            foreach (Molecule child in mole) {
                if (child != null) {
                    if (!string.IsNullOrEmpty(child._name)) objToDelete.Add(createText("<b>" + child._name + "</b>"));
                    if (!string.IsNullOrEmpty(child._class) && child._class.Contains("]")) {
                        string[] comp = child._class.Split(char.Parse("["));
                        comp[1].Substring(0,comp[1].Length-1);
                        objToDelete.Add(createHyper(comp));
                    }
                    else {
                        if (!string.IsNullOrEmpty(child._class))objToDelete.Add(createText(child._class));
                    }
                    if(Resources.Load<Sprite>("2DMolecules/"+child._name.Replace(" ", ""))) {
                        Debug.Log("<color=green>2DMolecules/"+child._name.Replace(" ", "") +"</color>");
                        objToDelete.Add(createImage("2DMolecules/"+child._name.Replace(" ", "")));
                    }
                    else {
                        Debug.Log("<color=red>2DMolecules/"+child._name.Replace(" ", "") +"</color>");
                    }
                    if (Resources.Load<GameObject>("Models/" + child._name)) objToDelete.Add(createAR(i));
                    if (!string.IsNullOrEmpty(child._description))objToDelete.Add(createText(child._description+ "\n"));
                    i++;
                }
            }
        }
        if (!string.IsNullOrEmpty(hardiness)) {
            objToDelete.Add(createText("<b>Hardiness Zone " + hardiness + "\n"));
            objToDelete.Add(createMap(hardiness));
        }
        
        if (!string.IsNullOrEmpty(toxicity)) {
            objToDelete.Add(createText("<b>Toxicity"));
            objToDelete.Add(createText(toxicity + "\n"));
        }
        if (!string.IsNullOrEmpty(cont))
        {
            objToDelete.Add(createText("<b>Contrindications</b>"));
            objToDelete.Add(createText(cont + "\n"));
        }
        if (!string.IsNullOrEmpty(links))
        {
            //Create an instance of a clickable hyperlink
            objToDelete.Add(createText("<b>Links"));
            string[] linkArray = links.Split(' ');
            //if(linkArray[0][0] == '[') linkArray[0] = linkArray[0].Substring(1,linkArray[0].Length);
            foreach (string link in linkArray) {
                string tmp = link;
                if (tmp[0] == '[') tmp = tmp.Substring(1, tmp.Length-1);
                if (tmp[link.Length-1] == ']') tmp = tmp.Substring(0, tmp.Length-1);
                objToDelete.Add(createHyper(new string[2] {tmp, tmp}));
            }
        }
    }

    public void loseFocus() {
        if (dynMol != null) dynMol.deleteTrackable();
        foreach(GameObject obj in objToDelete) {
            Destroy(obj);
        }
    }

    public Plant getPlant() {
        return currentPlant;
    }

    public Molecule getMole() {
        return currentMole;
    }

    public void closeAR() {
        if (dynMol != null) dynMol.deleteTrackable();
    }

    #endregion //PUBLIC_METHODS
    #region PRIVATE_METHODS
    private GameObject createText(string s) {
        GameObject tmp = Instantiate(Resources.Load<GameObject>("PlaceableText"));
        tmp.transform.SetParent(GameObject.Find("Canvas").transform.Find("InformationPanel").GetChild(0).GetChild(0).GetChild(0));
        tmp.transform.localScale = Vector3.one;
        tmp.GetComponent<TextMeshProUGUI>().text = s + "\n";
        return tmp;
    }

    private GameObject createHyper(string[] s) {
        GameObject hyperlink = Instantiate(Resources.Load<GameObject>("Hyperlink"));
        hyperlink.transform.SetParent(GameObject.Find("Canvas").transform.Find("InformationPanel").GetChild(0).GetChild(0).GetChild(0));
        hyperlink.transform.localScale = Vector3.one;
        hyperlink.transform.GetChild(0).GetComponent<ClickableText>().link = s[1];
        hyperlink.transform.GetComponent<TextMeshProUGUI>().SetText(s[0]);
        return hyperlink;
    }

    private GameObject createAR(int x) {
        GameObject arButton = Instantiate(Resources.Load<GameObject>("MoleculeButton"));
        arButton.GetComponent<ARButton>().index = x;
        arButton.transform.SetParent(GameObject.Find("Canvas").transform.Find("InformationPanel").GetChild(0).GetChild(0).GetChild(0));
        arButton.transform.localScale = Vector3.one;
        return arButton;
    }

    private GameObject createMap(string h) {
        GameObject hardinessMap = Instantiate(Resources.Load<GameObject>("HardinessMap"));
        hardinessMap.transform.SetParent(GameObject.Find("Canvas").transform.Find("InformationPanel").GetChild(0).GetChild(0).GetChild(0));
        hardinessMap.transform.localScale = Vector3.one;
        hardinessMap.GetComponent<HardinessMap>().updateMap(h);
        return hardinessMap;
    }

    private GameObject createImage(string n) {
        GameObject image = Instantiate(Resources.Load<GameObject>("PlaceableImage"));
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>(n);
        image.transform.SetParent(GameObject.Find("Canvas").transform.Find("InformationPanel").GetChild(0).GetChild(0).GetChild(0));
        image.transform.localScale = Vector3.one;
        return image;
    }
    #endregion //PRIVATE_METHODS
}
