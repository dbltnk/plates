using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOnMove : MonoBehaviour {

    GameObject gameplayRoot;
    Gameplay gp;
    Button button;

    // Use this for initialization
    void Start () {
        gameplayRoot = GameObject.Find("GameplayRoot");
        gp = gameplayRoot.GetComponent<Gameplay>();
        button = GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gp != null) {
            if (gp.CameraIsMoving) {
                button.interactable = false;
            }
            else {
                button.interactable = true;
            } 
        }
    }
}
