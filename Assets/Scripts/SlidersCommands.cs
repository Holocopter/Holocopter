using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersCommands : MonoBehaviour {
    PistonsController pc;
    GameObject SpeedSlider;
    GameObject CollectiveSlider;
    GameObject SizeSlier;
    GameObject MainCamera;
    public Slider speedSlider;
    public Slider collectiveSlider;
    public Slider sizeSlider;
	// Use this for initialization
	void Start () {
        pc = GameObject.FindObjectOfType<PistonsController>();
        SpeedSlider = GameObject.Find("SpeedSlider");
        speedSlider = SpeedSlider.GetComponent<Slider>();
        CollectiveSlider = GameObject.Find("CollectiveSlider");
        collectiveSlider = CollectiveSlider.GetComponent<Slider>();
        SizeSlier = GameObject.Find("SizeSlider");
        sizeSlider = SizeSlier.GetComponent<Slider>();
        MainCamera = GameObject.Find("MixedRealityCameraParent");
	}
	
	// Update is called once per frame
	void Update () {
        pc.sp = speedSlider.value;
        pc.collectivey = collectiveSlider.value;
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, sizeSlider.value);
        
    }

    void Speed_slider() {
       
    }
}
