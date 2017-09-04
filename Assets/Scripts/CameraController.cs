using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector2 cameraEdge1, cameraEdge2;

    public float dragSpeed = 2;
    float originaldragSpeed;
    Vector3 mouseStartPos;
    bool dragging;
    float dragtimer;

    bool somethingWithFinger;

	void Start () {
        originaldragSpeed = dragSpeed;
    }
	
	void FixedUpdate () {

        if (!somethingWithFinger) {
            if (transform.position.x > cameraEdge2.x || transform.position.x < cameraEdge1.x || transform.position.z > cameraEdge2.y || transform.position.z < cameraEdge1.y) {
                transform.Translate((cameraDollyCenter - transform.localPosition) * Time.fixedDeltaTime, Space.World);
            }
        }

        if (Input.touchCount > 0 && ScreenManager.Instance.currentScreenType == ScreenType.None) {

            somethingWithFinger = true;

            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                mouseStartPos = Input.mousePosition;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                Vector2 dif = Input.mousePosition - mouseStartPos;
                Vector3 pos = Camera.main.ScreenToViewportPoint(dif);
                Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);

                dragging = Mathf.Abs(dif.x) > 1.8f || Mathf.Abs(dif.y) > 1.8f;

                if (transform.position.x > cameraEdge2.x || transform.position.x < cameraEdge1.x || transform.position.z > cameraEdge2.y || transform.position.z < cameraEdge1.y) {
                    dragSpeed = 2;
                } else {
                    dragSpeed = originaldragSpeed;
                }

                if (dragging)
                    dragtimer += Time.fixedDeltaTime;

                if (dragtimer >= 0.15f)
                    dragging = true;

                mouseStartPos = Input.mousePosition;

                transform.Translate(move);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                if (!dragging) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 70, LayerMask.GetMask("Building"))) {
                        if (hit.collider != null) {
                            if (hit.collider.GetComponent<Building>()) {
                                Building b = hit.collider.GetComponent<Building>();
                                ScreenManager.Instance.ClickBuilding(b);
                            }
                        }
                    }
                }
                dragtimer = 0;
                dragging = false;
                somethingWithFinger = false;
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireCube(cameraDollyCenter, new Vector3( cameraEdge2.x - cameraEdge1.x, 1, cameraEdge2.y - cameraEdge1.y));
    }

    Vector3 cameraDollyCenter {
        get {
            return new Vector3((cameraEdge1.x + cameraEdge2.x) / 2, 0, (cameraEdge1.y + cameraEdge2.y) / 2);
        }
    }
}
