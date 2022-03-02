using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Quiz : MonoBehaviour
{
    private static int score = 0;
    private static int questionCount = 0;
    private static int colComName;
    private static int colDescription;
    private static int colFamily;
    private static int colSciName;
    private static List<int> currentIndexes = new List<int>();
    private static int correctIndex;
    private static System.Random r = new System.Random();
    private static List<bool> isCorrect = new List<bool>(new bool[4]);
    private static List<bool> isClicked = new List<bool>(new bool[4]);
    private static int numClicks = 0;

    public void startQuiz()
    {
        colComName = PlantManager.getColComName();
        colDescription = PlantManager.getColDescription();
        colFamily = PlantManager.getColFamily();
        colSciName = PlantManager.getColSciName();
        //set active false myButtonse
        /*
         ___________________________________________
         |    Question "number set by program"      |  -> one text box center aligned
         |                                          |
         |         "Question text here":            |                     
         |                                          |
         |                                          |
         |                                          |
         |                                          |
         |   _____________________                  |
         |  |A: Random Plant Name |                 |
         |  |_____________________|                 |
         |   _____________________                  |
         |  |B: Random Plant Name |                 |
         |  |_____________________|                 |  -> left alligned buttons
         |   _____________________                  |
         |  |C: Random Plant Name |                 |
         |  |_____________________|                 |
         |   _____________________                  |
         |  |D: Random Plant Name |                 |
         |  |_____________________|                 |
         |                                          |
         |                                ______    |  
         |                               | Next |   |   
         |                               |______|   |  -> right alligned next button
         |__________________________________________|
        
        */
        if (questionCount == 10)
        {
            Debug.Log("Final Score: " + score + "%");
            //display score pane
            resetQuiz();
        }

        //set all buttons to unclicked (false), and all buttons to incorrect (false)
        isCorrect = new List<bool>(new bool[4]);
        isClicked = new List<bool>(new bool[4]);
        numClicks = 0;
        GameObject.Find("Next").GetComponentInChildren<TextMeshProUGUI>().text = "Submit";
        GameObject.Find("Button1").GetComponentInChildren<Image>().color = Color.white;
        GameObject.Find("Button2").GetComponentInChildren<Image>().color = Color.white;
        GameObject.Find("Button3").GetComponentInChildren<Image>().color = Color.white;
        GameObject.Find("Button4").GetComponentInChildren<Image>().color = Color.white;

        Debug.Log("Current Score: " + score + "%");

        string[] questionTypes = { "CommonToSci", "" };

        int questionNum = r.Next(0, questionTypes.Length - 1); //this grabs which type of question will be asked


        string questionHeading = "\nQuestion " + (++questionCount).ToString() + "\n\n";
        Debug.Log(questionCount);

        Debug.Log(questionHeading);
        GameObject.Find("Heading").GetComponentInChildren<TextMeshProUGUI>().text = questionHeading;
        //set questionHeading


        //***************** set text box top equal to question******************

        correctIndex = getRandomPlantIndex();
        currentIndexes.Add(correctIndex);


        string question = "Choose the correct Scientific Name for " + Plant.matrix[correctIndex][colComName] + ":";

        //set question
        Debug.Log(question);
        GameObject.Find("Question Text").GetComponent<TextMeshProUGUI>().text = question;


        string correctAnswer = Plant.matrix[correctIndex][colSciName];

        Debug.Log("correct answer = " + correctAnswer);

        string buttonText;

        int unicode = 64; //for converting i into A, B, C, D

        int answerNum = r.Next(0, 4); // decides what button will have correct answer -> 0 will be 'A', 1 will be 'B', etc....

        for (int i = 0; i < 4; i++) // foreach button, i is used for unicode, there are four buttons totaL
        {
            if (i == answerNum) //correct button
            {
                buttonText = correctAnswer;
                isCorrect[i] = true;
            }
            else buttonText = Plant.matrix[getRandomAnswer(ref currentIndexes)][colSciName]; //incorrect button

            //set button text
            Debug.Log(buttonText);
            if (i == 0) GameObject.Find("Button1").GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            else if (i == 1) GameObject.Find("Button2").GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            else if (i == 2) GameObject.Find("Button3").GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            else GameObject.Find("Button4").GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
        }
        currentIndexes.Clear();
    }


    /* populates incorect answers that don't repeat
     * @param list of indexes already taken (passed by reference)
     */

    int getRandomAnswer(ref List<int> indexes)
    {
        int result = getRandomPlantIndex();
        bool bFound = false;
        while (!bFound)
        {
            if (!indexes.Contains(result)) bFound = true;
            else result = getRandomPlantIndex();
        }
        indexes.Add(result); //add new index to current indexes
        return result;
    }

    int getRandomPlantIndex()
    {
        return r.Next(1, 15); //r.Next(1, Plant.matrix.Capacity - 1)  ->  if database is complete
    }

    public void button1Click()
    {

        Debug.Log("Button A Clicked!");
        isClicked[0] = !isClicked[0];
        if (isClicked[0]) GameObject.Find("Button1").GetComponentInChildren<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.85f);
        else GameObject.Find("Button1").GetComponentInChildren<Image>().color = Color.white;
    }

    public void button2Click()
    {
        Debug.Log("Button B Clicked!");
        isClicked[1] = !isClicked[1];
        if (isClicked[1]) GameObject.Find("Button2").GetComponentInChildren<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.85f);
        else GameObject.Find("Button2").GetComponentInChildren<Image>().color = Color.white;
    }

    public void button3Click()
    {
        Debug.Log("Button C Clicked!");
        isClicked[2] = !isClicked[2];
        if (isClicked[2]) GameObject.Find("Button3").GetComponentInChildren<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.85f);
        else GameObject.Find("Button3").GetComponentInChildren<Image>().color = Color.white;
    }

    public void button4Click()
    {
        Debug.Log("Button D Clicked!");
        isClicked[3] = !isClicked[3];
        if (isClicked[3]) GameObject.Find("Button4").GetComponentInChildren<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.85f);
        else GameObject.Find("Button4").GetComponentInChildren<Image>().color = Color.white;

    }

    public void buttonNextClick()
    {
        numClicks++;
        if (numClicks == 1)
        {
            for (int i = 1; i < 5; i++)
                if (isCorrect[i - 1]) GameObject.Find("Button" + i.ToString()).GetComponentInChildren<Image>().color = new Color(0.6f, 1f, 0.6f, 1f); //if correct answer turn green
            GameObject.Find("Next").GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        }
        else if (numClicks == 2)
        {
            bool correct = true;
            for (int i = 0; i < 4; i++)
                if (isClicked[i] != isCorrect[i]) correct = false; //incorrect clicked doesn't match what is correct
            if (correct) score += 10; //correct
            if (questionCount != 10) startQuiz();
            //set active score panel

            startQuiz();
        }
    }

    private void resetQuiz()
    {
        score = 0;
        questionCount = 0;
    }

    public void cameraButtonClick()
    {

        startQuiz();
    }

}