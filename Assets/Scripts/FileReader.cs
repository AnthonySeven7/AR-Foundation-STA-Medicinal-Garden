using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using TMPro;


public class FileReader : MonoBehaviour
{

    /* Function: read_File()
     * Brief: 1. Reads from TSV file
     *        2. Creates a Matrix that mirrors the excel sheet -> plantMatrix[row,column]
    */
    [SerializeField]
    private TextAsset fileToBeOpened;
    [SerializeField]
    private GameObject buttonPreFab;

    //make return type plantmatrix
    public List<List<string>> readFile()
    {
        List<List<string>> matrix = new List<List<string>>();
        #if UNITY_EDITOR
        string filePath = AssetDatabase.GetAssetPath(fileToBeOpened);
        string fileExt = string.Empty;
        fileExt = Path.GetExtension(filePath);
        #endif
        //if (fileExt.CompareTo(".txt") == 0)
        //{
            try
            {
                string[] arr;
                string line = "";
                int row = 0; //index for plants/lines
                #if UNITY_EDITOR
                StreamReader reader = new StreamReader(filePath);
                while ((line = reader.ReadLine()) != null)
                {
                    arr = line.Split('\u2605');
                    matrix.Add(new List<String>()); //adds sublist/row within matrix
                    for (int i = 0; i < arr.Length; i++) matrix[row].Add(arr[i]);//add data to current row
                    row++;
                }
                reader.Close();
                #else
                TextAsset text = Resources.Load<TextAsset>(fileToBeOpened.name);
                string[] linesFromFile = text.text.Split("\n"[0]);
                foreach(string _line in linesFromFile) {
                    arr = _line.Split('\u2605');
                    matrix.Add(new List<String>()); //adds sublist/row within matrix
                    for (int i = 0; i < arr.Length; i++) matrix[row].Add(arr[i]);//add data to current row
                    row++;
                }
                #endif
            }
            catch (Exception ex)
            {
                throw ex;
            }
        //}
        return matrix;
        /*
        foreach (List<string> subList in matrix)
        {

            Debug.Log("PLANT: ");
            foreach (string cell in subList)
                Debug.Log("COLUMN 1: " + cell); //first iteration will be cell[0,0] followed by [0,1] etc...
        }
        
        //option 2
        for (int row = 0; row < matrix.Count; row++)
        {
            for (int column = 0; column < matrix[row].Count; column++)
            {
                Console.WriteLine(matrix[row][column]); //first iteration will be cell[0,0] followed by [0,1] etc...
            }
        }*/
  
    }
}
