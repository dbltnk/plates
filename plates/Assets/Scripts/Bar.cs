using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ObjectCurrent.transform.localPosition = new Vector3(0.02f, currentY, -1.18f);
    }

    public void IncreaseCurrent() {
        Current += IncreasePerTap;
    }
}
