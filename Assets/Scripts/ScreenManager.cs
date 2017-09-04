using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class ScreenManager : MonoBehaviour {

    public static ScreenManager Instance;

    GameObject currentScreen;
    GameObject tobeScreen;

    [Header("Screens")]
    public GameObject screenBuildingStatus;
    public GameObject screenBuildingUpgrade;
    public GameObject screenBuildingNewOrder;
    public GameObject screenSettings;
    public GameObject screenResearch;

    [Header("Top Elements")]
    public Text textMoney;
    public Text textLevel;
    public Image imageExperience;

    [Header("Screen Settings")]
    [Range(1f, 3f)]
    public float screenSwapSpeed;
    public AnimationCurve animationCurve;

    public ScreenType currentScreenType = ScreenType.None;
    ScreenType workingScreenType = ScreenType.None;

    float timer;

    Building buildingInfo;
    BlurOptimized blur;

    void Awake() {
        Instance = this;
    }

	void Start () {
        blur = Camera.main.GetComponent<BlurOptimized>();
	}
	
	void Update () {
        if (currentScreenType != workingScreenType) {
            timer += Time.deltaTime * screenSwapSpeed;

            if(currentScreenType == ScreenType.None && currentScreen == null) {
                blur.blurSize = timer * 5;
            }

            if (currentScreenType != ScreenType.None && currentScreen != null) {
                currentScreen.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(600 * (workingScreenType != ScreenType.None ? -1 : 1), 0, 0), animationCurve.Evaluate(timer));
            }

            if(workingScreenType == ScreenType.None) {
                blur.blurSize = 5 - (timer * 5);
            }

            if (workingScreenType != ScreenType.None && tobeScreen != null) {
                tobeScreen.transform.localPosition = Vector3.Lerp(new Vector3(600, 0, 0), Vector3.zero, animationCurve.Evaluate(timer));
            }

            if (timer >= 1) {
                currentScreenType = workingScreenType;
                currentScreen = tobeScreen;
                timer = 0;

                if (currentScreenType == ScreenType.None) {
                    blur.blurSize = 0;
                } else {
                    blur.blurSize = 5;
                }
            }
        }

    }

    public void ClickType(ScreenType newtype) {
        workingScreenType = newtype;
        switch (workingScreenType) {
            case ScreenType.BuildingStatus: tobeScreen = screenBuildingStatus; break;
            case ScreenType.BuildingUpgrade: tobeScreen = screenBuildingUpgrade; break;
            case ScreenType.BuildingNewTank: tobeScreen = screenBuildingNewOrder; break;
            case ScreenType.Settings: tobeScreen = screenSettings; break;
            case ScreenType.Research: tobeScreen = screenResearch; break;
            default: tobeScreen = null; break;
        }
    }

    public void ClickCross() {
        ClickType(ScreenType.None);
    }

    public void ClickSettings() {
       ClickType(ScreenType.Settings);
    }

    public void ClickResearch() {
        ClickType(ScreenType.Research);
    }

    public void ClickBuildingUpgrade() {
        ClickType(ScreenType.BuildingUpgrade);
    }

    public void ClickNewOrder() {
        ClickType(ScreenType.BuildingNewTank);
    }

    public void ClickBuilding(Building b) {
        buildingInfo = b;
        ClickType(ScreenType.BuildingStatus);
        if (tobeScreen.GetComponent<BuildingStatus>())
            tobeScreen.GetComponent<BuildingStatus>().SetupBuilding(b);
    }
}

public enum ScreenType {
    None, BuildingStatus, BuildingUpgrade, BuildingNewTank, Settings, Research
}
