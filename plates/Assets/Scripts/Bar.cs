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
    }
}
