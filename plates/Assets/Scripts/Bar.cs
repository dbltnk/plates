using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    public string BarName;
    public float Min;
    public float Current;
    public float Max;
    public float DecayPerSecond;
    GameObject gameplayRoot;
    Gameplay gp;
    public float HotspotTop;
    public float HotspotBottom;
    public bool IsOnHotspot;
    public float ConnectednessPerSecondNormal;
    public float ConnectednessPerSecondHotspot;
    public float IncreasePerTap;
    public GameObject ObjectCurrent;
    public GameObject ObjectHotspot;
    public float hotspotRange;
    public float hotspotCenter;

    // https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    float MapIntoRange (float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    // Use this for initialization
    void Start () {
        gameplayRoot = GameObject.Find("GameplayRoot");
        gp = gameplayRoot.GetComponent<Gameplay>();
    }
	
	// Update is called once per frame
	void Update () {
        Current -= Time.deltaTime * DecayPerSecond;
        Current = Mathf.Min(Current, Max);
        Current = Mathf.Max(Current, Min);

        if (HotspotTop <= HotspotBottom) Debug.LogError("HotspotTop has to be larger than HotspotBottom.");

        if (Current <= HotspotTop && Current >= HotspotBottom) {
            IsOnHotspot = true;
        }
        else {
            IsOnHotspot = false;
        }

        if (gp != null) {
            if (IsOnHotspot) {
                gp.IncreaseConnectedness(ConnectednessPerSecondHotspot * Time.deltaTime);
            } else {
                gp.IncreaseConnectedness(ConnectednessPerSecondNormal * Time.deltaTime);
            }
        }

        // y for 0 -> -2.36
        // y for 1 -> 4.47
        float currentY = MapIntoRange(Current, Min, Max, -2.36f, 4.47f);
        ObjectCurrent.transform.localPosition = new Vector3(0f, currentY, -1.18f);

        // we do this math on update just so we can change this on runtime, maybe a bit slow but who cares
        hotspotRange = HotspotTop - HotspotBottom;
        hotspotCenter = HotspotTop - hotspotRange / 2f;

        float hotspotY = MapIntoRange(hotspotCenter, Min, Max, -2.36f, 4.47f);
        ObjectHotspot.transform.localPosition = new Vector3(0f, hotspotY, -0.96f);

        float hotspotHeight = MapIntoRange(hotspotRange, Min, Max, 0, 7f);
        ObjectHotspot.transform.localScale = new Vector3(1f, hotspotHeight, 0.3612825f);
    }

    public void IncreaseCurrent() {
        if (gp.EnergyCurrent > gp.EnergyCosts[gp.LocationCurrentID] &&
            gp.MoneyCurrent > gp.MoneyCosts[gp.LocationCurrentID]) {
            Current += IncreasePerTap;
            gp.DecreaseEnergy(gp.EnergyCosts[gp.LocationCurrentID]);
            gp.IncreaseMoney(gp.MoneyCosts[gp.LocationCurrentID] * -1);
        }
    }
}
