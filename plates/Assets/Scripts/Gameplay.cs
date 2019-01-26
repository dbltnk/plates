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
    public Text TextLocation;

    public Bar CurrentBar;
    public Slider EnergyBar;

    public float BarDistance;
    public Vector3 CameraTarget;
    public GameObject Camera;
    public bool CameraIsMoving;
    public float CameraTransitionDuration;

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
        EnergyBar.value = EnergyCurrent;

        int target = LocationCurrentID;
        CameraTarget = new Vector3(target * BarDistance - BarDistance, 1f, -10f);
    }
	
	// Update is called once per frame
	void Update () {
        TimeCurrent += Time.deltaTime;
        EnergyCurrent += Time.deltaTime * EnergyRefillPerSecond;
        EnergyCurrent = Mathf.Min(EnergyCurrent, EnergyMax);
        EnergyCurrent = Mathf.Max(EnergyCurrent, EnergyMin);
        EnergyBar.value = EnergyCurrent;
        LocationCurrentName = Locations[LocationCurrentID];

        TextTime.text = string.Format("Time: {0:0}", TimeCurrent);
        TextEnergy.text = string.Format("Energy: {0:0}", EnergyCurrent);
        TextConnectedness.text = string.Format("Connectedness: {0:0}", ConnectednessCurrent);
        TextLocation.text = string.Format("Location: {0}", LocationCurrentName);

        // probably very expensive but who cares
        LocationCurrentName = Locations[LocationCurrentID];
        GameObject barObject = GameObject.Find(LocationCurrentName);
        Bar bar = barObject.GetComponent<Bar>();
        bar.BarName = LocationCurrentName;
        CurrentBar = bar;

        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CameraTarget, 0.1f / CameraTransitionDuration);
        if (Camera.transform.position != CameraTarget) {
            CameraIsMoving = true;
        }
        else {
            CameraIsMoving = false;
        }
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
            CameraTarget = new Vector3(target * BarDistance - BarDistance, 1f, -10f);
        }
    }
}
