using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Search : MonoBehaviour
{
    public void checkText()
    {
        string inputText = GetComponent<TMP_InputField>().text.ToLower().Replace(" ", ""); //sets text in the InputField to a string
        string comName; //to be compared to inputText
        string sciName;
        string description;
        //loop through plants
        foreach (Plant plant in PlantManager.myPlants)
        {
            comName = plant.getComName().ToLower().Replace(" ", "");
            sciName = plant.getSciName().ToLower().Replace(" ", "");
            description = plant.getDesc().ToLower().Replace(" ", "");
            if (inputText == "") plant.gameObject.SetActive(true);                                                  //backspaced to empty inputText, activate buttons
            else if (comName.Contains(inputText) || sciName.Contains(inputText) || description.Contains(inputText)) plant.gameObject.SetActive(true);  //common or scientific name contains inputText, activate buttons
            else plant.gameObject.SetActive(false);                                                                 //deactivate buttons
        }
    }
}
