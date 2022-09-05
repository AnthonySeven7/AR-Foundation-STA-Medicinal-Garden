using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using TMPro;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageRecognition : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private static string trackedName;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        //Spawns each prefab in placeablePrefabs
        // foreach(GameObject prefab in placeablePrefabs)
        // {
        //     GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //     newPrefab.name = prefab.name;
        //     spawnedPrefabs.Add(prefab.name, newPrefab);
        // }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    //Functionality to images appearing and disappearing
    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        // foreach (ARTrackedImage trackedImage in eventArgs.removed)
        // {
        //     spawnedPrefabs[trackedImage.name].SetActive(false);
        // }
    }


    private void UpdateImage(ARTrackedImage trackedImage)
    {
        //Match prefab image positions and visibility
        trackedName = trackedImage.referenceImage.name;     //*************************************image name

        //findIndex(trackedName);

        string plantName = GameObject.Find("PlantManager").GetComponent<PlantManager>().getPlant().name;
        //Vector3 position = trackedImage.transform.position;

        //GameObject prefab = spawnedPrefabs[name];
        //prefab.transform.position = position;
        //prefab.SetActive(true);
        GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = plantName.ToLower().Substring(0,plantName.Length-7) + "|" + trackedName.ToLower();
        GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += trackedName;
        if (plantName.ToLower().Substring(0,plantName.Length-7) != trackedName.ToLower()) {
            //GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += "\nfalse";
            GameObject.Find("PlantManager").GetComponent<PlantManager>().dynMol.gameObject.SetActive(false);
        }
        else {
            //GameObject.Find("DEBUGPANEL").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += "\ntrue";
            GameObject.Find("PlantManager").GetComponent<PlantManager>().dynMol.gameObject.SetActive(true);
        }
        // foreach(GameObject go in spawnedPrefabs.Values)
        // {
        //     if(go.name != name)
        //     {
        //         go.SetActive(false);
        //     }
        // }
    }


    public static string getTrackedName()
    {
        return trackedName;
    }
    /*
    private void findIndex(string name)
    {

    }
    */
}
