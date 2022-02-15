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

    public List<List<string>> readFile()
    {
        List<List<string>> matrix = new List<List<string>>();
        #if UNITY_EDITOR
        string filePath = AssetDatabase.GetAssetPath(fileToBeOpened);
        string fileExt = string.Empty;
        fileExt = Path.GetExtension(filePath);
        #endif
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
        return matrix;
    }
}
