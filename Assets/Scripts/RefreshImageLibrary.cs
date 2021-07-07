using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RefreshImageLibrary : MonoBehaviour
{
    [MenuItem("AR Tools/Refresh Image Library")]
    static void Refresh() {
        Debug.Log("Refreshing Image Library...");
        ARTrackedImageManager manager = GameObject.Find("AR Session Origin").GetComponent<ARTrackedImageManager>();
        if (manager == null)
        {
            Debug.Log("ERROR: Image Library not found");
        }
        else if (manager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary) {
            mutableLibrary.ScheduleAddImageJob(null,"test",0.5f);
            Debug.Log("Mutable");
        }
        else {
            Debug.Log("ERROR: Library is not mutable");
        }
    }
}
