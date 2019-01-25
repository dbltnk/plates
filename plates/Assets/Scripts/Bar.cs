using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    public string BarName;
    public float Min;
    public float Current;
    public float Max;
    public float DecayPerSecond;
    public float Hotspot;
    public Gameplay Gp;
    public bool IsOnHotSpot;
    public bool IsOverHotSpot;
    public bool IsUnderHotSpot;
    public float HotSpotToTop;
    public float HotSpotToBottom;

    // Use this for initialization
    void Start () {
        HotSpotToTop = Max - Hotspot;
        HotSpotToBottom = Min + Hotspot;
    }
	
	// Update is called once per frame
	void Update () {
        Current -= Time.deltaTime * DecayPerSecond;
        Current = Mathf.Min(Current, Max);
        Current = Mathf.Max(Current, Min);

        if (Current == Hotspot) {
            IsOnHotSpot = true;
        }
        else {
            IsOnHotSpot = false;
        }

        if (Current > Hotspot) {
            IsOverHotSpot = true;
        } else {
            IsOverHotSpot = false;
        }

        if (Current < Hotspot) {
            IsUnderHotSpot = true;
        } else {
            IsUnderHotSpot = false;
        }

        // TODO: CALCULATE HOW MUCH POINTS YOU GET DEPENDING ON HOW FAR AWAY YOUR ARE

    }
}
