using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingStatus : MonoBehaviour {

    Building currentBuilding;

    [Header("Elements")]
    public Text buildingNameText;
    public GameObject HasOrderObject;
    public GameObject noOrderObject;
    public Text orderTimeText;

	void Start () {
		
	}
	
	void FixedUpdate () {
        if (currentBuilding != null)
            orderTimeText.text = currentBuilding.currentOrderTime.GetTime();
    }

    public void SetupBuilding(Building b) {
        currentBuilding = b;
        Setup();
    }

    void Setup() {
        buildingNameText.text = currentBuilding.buildingName;

        HasOrderObject.SetActive(currentBuilding.hasOrder);
        noOrderObject.SetActive(!currentBuilding.hasOrder);
    }
}
