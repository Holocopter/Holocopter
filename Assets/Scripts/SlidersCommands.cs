using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersCommands : MonoBehaviour {
    PistonsController pc;
    GameObject SpeedSlider;
    GameObject CollectiveSlider;
    public Slider speedSlider;
    public Slider collectiveSlider;
	// Use this for initialization
	void Start () {
        pc = GameObject.FindObjectOfType<PistonsController>();
        SpeedSlider = GameObject.Find("SpeedSlider");
        speedSlider = SpeedSlider.GetComponent<Slider>();
        CollectiveSlider = GameObject.Find("CollectiveSlider");
        collectiveSlider = CollectiveSlider.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        pc.sp = speedSlider.value;
        pc.collectivey = collectiveSlider.value;
    }

    void Speed_slider() {
       
    }
}
