using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Building : MonoBehaviour {

    public string buildingName;

    public OrderTime originalOrderTime;
    public OrderTime currentOrderTime;

    public int orderAmount;
    public float orderTime;

    float timer;

    public Transform uiBuildingItem;
    public Transform uiFollower;

	void Start () {
		
	}
	
	void FixedUpdate () {
        if (hasOrder) {
            timer += Time.fixedDeltaTime;
            if (timer >= 1) {
                timer -= 1;
                currentOrderTime.RemoveSecond();
            }

            if (!currentOrderTime.HasTime()) {
                print("done");
            }

            uiBuildingItem.position = Camera.main.WorldToScreenPoint(uiFollower.position);
            uiBuildingItem.GetChild(1).GetComponent<Text>().text = currentOrderTime.GetTime();
            uiBuildingItem.GetChild(0).GetComponent<Image>().fillAmount = currentOrderTime.Percent(originalOrderTime);
        }
	}

    [ContextMenu("Place Order")]
    public void Order() {
        //placeOrder(10, 0.97f);
        ScreenManager.Instance.ClickNewOrder();
    }

    public void placeOrder(int amount, float timer) {
        orderAmount = amount;
        orderTime = timer;
        double time = orderAmount * orderTime;

        originalOrderTime = new OrderTime(time);
        currentOrderTime = originalOrderTime;
    }

    public bool hasOrder {
        get {
            return currentOrderTime.HasTime();
        }
    }
}
