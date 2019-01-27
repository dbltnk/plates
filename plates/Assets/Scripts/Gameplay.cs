using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour {

    public float MoneyCurrent;

    public float EnergyMin;
    public float EnergyCurrent;
    public float EnergyMax;
    public float EnergyRefillPerSecond;

    public float ConnectednessCurrent;
    public float ConnectednessDecayPerSecond;

    public Dictionary<int, string> Locations = new Dictionary<int, string>();
    public int LocationCurrentID;
    public string LocationCurrentName;
    public Dictionary<int, float> EnergyCosts = new Dictionary<int, float>();
    public Dictionary<int, float> MoneyCosts = new Dictionary<int, float>();
    public Dictionary<int, string> Actions = new Dictionary<int, string>();
    public Dictionary<int, float> Effects = new Dictionary<int, float>();

    public GameObject PrefabBar;

    public Text TextMoney;
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

    public float MoneyGainPerSecond;

    // Use this for initialization
    void Start () {
        Locations.Add(0, "Hometown");
        EnergyCosts.Add(0, 95f);
        MoneyCosts.Add(0, 500f);
        Actions.Add(0, "Long Stay");
        Effects.Add(0, 85f);

        Locations.Add(1, "Home Country");
        EnergyCosts.Add(1, 50f);
        MoneyCosts.Add(1, 100f);
        Actions.Add(1, "Weekend Visit");
        Effects.Add(1, 45f);

        Locations.Add(2, "New Home");
        EnergyCosts.Add(2, 10f);
        MoneyCosts.Add(2, 5f);
        Actions.Add(2, "Call");
        Effects.Add(2, 5f);

        Locations.Add(3, "Work");
        EnergyCosts.Add(3, 20f);
        MoneyCosts.Add(3, 0f);
        Actions.Add(3, "Work");
        Effects.Add(3, 10f);

        Locations.Add(4, "Socialising");
        EnergyCosts.Add(4, 25f);
        MoneyCosts.Add(4, 50f);
        Actions.Add(4, "Dance");
        Effects.Add(4, 20f);

        foreach (KeyValuePair<int, string> e in Locations) {
            GameObject o = Instantiate(PrefabBar);
            o.transform.localPosition = new Vector3(e.Key * BarDistance - BarDistance, 0f, 0f);
            o.GetComponent<Bar>().BarName = e.Value;
            o.name = e.Value;
        }

        LocationCurrentID = 2;
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
        EnergyCurrent += Time.deltaTime * EnergyRefillPerSecond;
        EnergyCurrent = Mathf.Min(EnergyCurrent, EnergyMax);
        EnergyCurrent = Mathf.Max(EnergyCurrent, EnergyMin);
        EnergyBar.value = EnergyCurrent;
        LocationCurrentName = Locations[LocationCurrentID];

        TextMoney.text = string.Format("Money: {0:0}", MoneyCurrent);
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

        ConnectednessCurrent -= ConnectednessDecayPerSecond * Time.deltaTime;
        ConnectednessCurrent = Mathf.Max(ConnectednessCurrent, 0);

        if (LocationCurrentID == 3) {
            MoneyCurrent += MoneyGainPerSecond * Time.deltaTime;
        };
    }

    public void IncreaseConnectedness(float v) {
        ConnectednessCurrent += v;
    }

    public void IncreaseMoney (float v) {
        MoneyCurrent += v;
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
