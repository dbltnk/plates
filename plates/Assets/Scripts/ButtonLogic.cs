using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour {

    GameObject gameplayRoot;
    Gameplay gp;
    Bar bar;
    Text buttonText;

    // Use this for initialization
    void Start () {
        gameplayRoot = GameObject.Find("GameplayRoot");
        gp = gameplayRoot.GetComponent<Gameplay>();
        buttonText = GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gp != null) {
            bar = gp.CurrentBar;
            if (buttonText != null) {
                buttonText.text = string.Concat(gp.Actions[gp.LocationCurrentID], "\n(",
                                                gp.EnergyCosts[gp.LocationCurrentID], "e) (",
                                                gp.MoneyCosts[gp.LocationCurrentID], "m)"
                    ); 
            }
        }
    }

    public void Tap () {
        bar.IncreaseCurrent();
    }
}
