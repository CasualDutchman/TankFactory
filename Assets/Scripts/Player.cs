using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

    public Money playerMoney;
    public int playerLevel = 1;
    public int playerExperience;

    ScreenManager screenManager;

    void Awake() {
        Instance = this;
        LoadFromSave();
    }

	void Start () {
        screenManager = ScreenManager.Instance;

        playerMoney = new Money(999, 999, 999);
    }
	
	void Update () {
        screenManager.textMoney.text = "$" + playerMoney.ToString() + "";

        //playerMoney.Add(new Money(11.11f));
    }
    
    [ContextMenu("Test Money")]
    public void AddMoney() {
        print(playerMoney.Remove(new Money(0, 0, 0, 1)));
    }

    public void AddExperience(int amount) {
        playerExperience += amount;
        if (playerExperience >= experienceTillNewLevel) {
            playerExperience -= experienceTillNewLevel;
            playerLevel++;
            screenManager.textLevel.text = playerLevel + "";
        }
        screenManager.imageExperience.fillAmount = playerExperience / (float)experienceTillNewLevel;
    }

    int experienceTillNewLevel {
        get {
            return playerLevel * 20;
        }
    }

    void LoadFromSave() {
        print("Load save");
    }
}
