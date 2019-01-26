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

    public float BarDistance;

    // Use this for initialization
    void Start () {
        Locations.Add(0, "Munich");
        Locations.Add(1, "Apartment");
        Locations.Add(2, "Work");

        foreach (KeyValuePair<int, string> e in Locations) {
            GameObject o = Instantiate(PrefabBar);
            o.transform.localPosition = new Vector3(e.Key * BarDistance - BarDistance, 0f, 0f);
            o.GetComponent<Bar>().BarName = e.Value;
            o.name = e.Value;
        }

        LocationCurrentID = 1;
        LocationCurrentName = Locations[LocationCurrentID];
        GameObject barObject = GameObject.Find(LocationCurrentName);
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

        // probably very expensive but who cares
        LocationCurrentName = Locations[LocationCurrentID];
        GameObject barObject = GameObject.Find(LocationCurrentName);
        Bar bar = barObject.GetComponent<Bar>();
        bar.BarName = LocationCurrentName;
        CurrentBar = bar;
    }

    public void IncreaseConnectedness(float v) {
        ConnectednessCurrent += v;
    }

    public void DecreaseEnergy (float v) {
        EnergyCurrent -= v;
        EnergyCurrent = Mathf.Min(EnergyCurrent, EnergyMax);
        EnergyCurrent = Mathf.Max(EnergyCurrent, EnergyMin);
    }

    public void ChangeLocation (int direction) {
        int target = LocationCurrentID + direction;
        if (target >= 0 && target <= Locations.Count - 1 ) {
            LocationCurrentID += direction;
            LocationCurrentName = Locations[LocationCurrentID];
        }
    }
}
