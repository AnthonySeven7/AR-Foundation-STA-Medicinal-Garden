using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject ARPanel;
    public GameObject InformationPanel;
    public GameObject MainPanel;
    public GameObject ARCamera;
    public PlantManager PlantManager;
    public void onClick(){
        ARPanel.gameObject.SetActive(false);
        InformationPanel.gameObject.SetActive(true);
        MainPanel.gameObject.SetActive(true);
        ARCamera.gameObject.SetActive(false);
        PlantManager.closeAR();
        if (GameObject.Find("DynamicTrackable(Clone)")) {
        GameObject.Destroy(GameObject.Find("DynamicTrackable(Clone)"));
        }
    }
}
