using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideOnMove : MonoBehaviour {

    GameObject gameplayRoot;
    Gameplay gp;
    Button button;
    SpriteRenderer sR;

    // Use this for initialization
    void Start () {
        gameplayRoot = GameObject.Find("GameplayRoot");
        gp = gameplayRoot.GetComponent<Gameplay>();
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        if (gp != null) {
            if ((gp.CameraIsMoving)) {
                sR.enabled = false;
            } else {
                sR.enabled = true;
            }
        }
    }
}