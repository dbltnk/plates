using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour {

    GameObject gameplayRoot;
    Gameplay gp;
    Bar bar;

    // Use this for initialization
    void Start () {
        gameplayRoot = GameObject.Find("GameplayRoot");
        gp = gameplayRoot.GetComponent<Gameplay>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gp != null) {
            bar = gp.CurrentBar;
        }
    }

    public void Tap () {
        bar.IncreaseCurrent();
    }
}
