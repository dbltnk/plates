using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableOnEnd : MonoBehaviour {

    GameObject gameplayRoot;
    Gameplay gp;
    Button button;
    public bool IsLeft;
    public bool DisableMe;

    // Use this for initialization
    void Start () {
        gameplayRoot = GameObject.Find("GameplayRoot");
        gp = gameplayRoot.GetComponent<Gameplay>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update () {
        if (gp != null) {
            if ((gp.LocationCurrentID <= 0 && IsLeft) || (gp.LocationCurrentID >= gp.Locations.Count -1 && !IsLeft)) {
                DisableMe = true;
            } else {
                DisableMe = false;
            }
        }
    }
}
