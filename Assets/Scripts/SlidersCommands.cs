using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Examples.InteractiveElements;

public class SlidersCommands : MonoBehaviour
{
    PistonsController pc;
    GameObject SpeedSlider;
    GameObject CollectiveSlider;
    GameObject SizeSlier;
    GameObject MainCamera;
    public SliderGestureControl speedSlider;
    public SliderGestureControl collectiveSlider;

    public SliderGestureControl sizeSlider;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindObjectOfType<PistonsController>();
        SpeedSlider = GameObject.Find("SpeedSlider");
        speedSlider = SpeedSlider.GetComponent<SliderGestureControl>();
        CollectiveSlider = GameObject.Find("CollectiveSlider");
        collectiveSlider = CollectiveSlider.GetComponent<SliderGestureControl>();
        SizeSlier = GameObject.Find("SizeSlider");
        sizeSlider = SizeSlier.GetComponent<SliderGestureControl>();
        MainCamera = GameObject.Find("MixedRealityCameraParent");
    }

    // Update is called once per frame
    void Update()
    {
        pc.sp = speedSlider.SliderValue;
        pc.collectivey = collectiveSlider.SliderValue;
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,
            sizeSlider.SliderValue);
    }

    public void OnMakeFaster()
    {
        Debug.Log("Faster");
        var step = (speedSlider.MaxSliderValue - speedSlider.MinSliderValue) / 5;
        speedSlider.SetSliderValue(speedSlider.SliderValue + step);
    }

    public void OnMakeSlower()
    {
        Debug.Log("Smaller");
        var step = (speedSlider.MaxSliderValue - speedSlider.MinSliderValue) / 5;
        speedSlider.SetSliderValue(speedSlider.SliderValue - step);
    }

    void Speed_slider()
    {
    }
}