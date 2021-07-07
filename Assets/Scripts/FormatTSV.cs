using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
/// <summary>
/// This script is a tool that will allow the reformatting of a TSV file into a file
/// delimited by a star. This doesn't effect any of the coding that reads a TSV but
/// it will make the file easier to read in a text editor.
/// </summary>
public class FormatTSV : EditorWindow
{
    [MenuItem("Assets/Format TSV", false, 200)]
    public static void ShowWindow() {
        // Get the selected file's path
        Debug.Log("Formatting TSV");
        var path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
        StreamReader fileData = new StreamReader(path); // Open the TSV file provided and prepare to start reading
        string line = fileData.ReadToEnd(); // Read the entire file
        // Replace tabs with stars for easier reading
        line = line.Replace("\t", "\u2605");
        line = line.Replace("\u2605 ", "\u2605");
        line = line.Replace("\u2605  ", "\u2605");
        line = line.Replace(" \u2605", "\u2605");
        line = line.Replace("  \u2605", "\u2605");
        fileData.Close(); // Close file
        StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Truncate)); // Open the file for writing
        writer.Write(line); // Write to file
        writer.Close(); // Close file
    }

    [MenuItem("Assets/Format TSV", true, 200)]
    public static bool ShowWindowTest() { // Ensure the selected file is a TSV
        var obj = Selection.activeObject;
        if (obj == null) return false;
        var path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
        return path.Substring(path.Length-4) == ".tsv";
    }
}
