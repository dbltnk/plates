using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour {

    public float TimeCurrent;

    public float EnergyMin;
    public float EnergyCurrent;
    public float EnergyMax;
    public float EnergyRefillPerSecond;

    public float ConnectednessCurrent;

    public Dictionary<int, string> Locations = new Dictionary<int, string>();
    public int LocationCurrentID;
    public string LocationCurrentName;

    public GameObject PrefabBar;

    public Text TextTime;
    public Text TextEnergy;
    public Text TextConnectedness;

    public Bar CurrentBar;

    // Use this for initialization
    void Start () {
        Locations.Add(-1, "Munich");
        Locations.Add(0, "Apartment");
        Locations.Add(1, "Work");
        LocationCurrentID = 0;
        LocationCurrentName = Locations[LocationCurrentID];
        GameObject barObject = Instantiate(PrefabBar);
        Bar bar = barObject.GetComponent<Bar>();
        bar.BarName = LocationCurrentName;
        CurrentBar = bar;
    }
	
	// Update is called once per frame
	void Update () {
        TimeCurrent += Time.deltaTime;
        EnergyCurrent += Time.deltaTime * EnergyRefillPerSecond;
        EnergyCurrent = Mathf.Min(EnergyCurrent, EnergyMax);
        EnergyCurrent = Mathf.Max(EnergyCurrent, EnergyMin);
        LocationCurrentName = Locations[LocationCurrentID];

        TextTime.text = string.Concat("Time: ", TimeCurrent);
        TextEnergy.text = string.Concat("Energy: ", EnergyCurrent);
        TextConnectedness.text = string.Concat("Connectedness: ", ConnectednessCurrent);
    }

    public void IncreaseConnectedness(float v) {
        ConnectednessCurrent += v;
    }
}
