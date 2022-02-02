using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardinessMap : MonoBehaviour
{
    #region PUBLIC_MONOBEHAVIOUR_METHODS
    public void updateMap(string hardiness) {
        string[] str_zones = hardiness.Split(' ');
        int[] zones = new int[str_zones.Length];
        for (int i = 0; i < str_zones.Length; i++) {
            int.TryParse(str_zones[i], out zones[i]);
        }
        switch (zones.Length) {
            case 0: // if no hardiness was provided
                destroyMap();
                break;
            case 1: // if one hardiness zone was provided
                for (int i = 1; i < transform.childCount; i++) {
                    if (i != zones[0]) {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
            case 3: // if two hardiness zones were provided
                for (int i = 1; i < transform.childCount; i++) {
                    if (i < Mathf.Min(zones[0], zones[2]) || i > Mathf.Max(zones[0], zones[2])) {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
        }
    }

    public void destroyMap() {
        Debug.Log("Destroy Map");
        Destroy(this.gameObject);
    }
    #endregion // PUBLIC_MONOBEHAVIOUR_METHODS
}
