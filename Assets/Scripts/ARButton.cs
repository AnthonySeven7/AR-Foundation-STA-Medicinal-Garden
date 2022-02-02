﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARButton : MonoBehaviour
{
    public int index = 0;

    [SerializeField]
    public void onClick() {
        GameObject.Find("Canvas").transform.Find("InformationPanel").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("ARPanel").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("MainPanel").gameObject.SetActive(false);
        if (GameObject.Find("DynamicTrackable")) GameObject.Find("DynamicTrackable").GetComponent<DynamicMole>().createTrackable(index);
        GameObject.Find("AR Session Origin").transform.GetChild(0).gameObject.SetActive(true);
    }
}
