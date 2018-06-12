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
    GameObject AirFlow;
    GameObject MainCamera;
    
    public SliderGestureControl speedSlider;
    public SliderGestureControl collectiveSlider;
    public SliderGestureControl sizeSlider;
    public ButtonEvent airFlow;

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
        AirFlow = GameObject.Find("WindFx");
        airFlow = AirFlow.GetComponent<ButtonEvent>();
        MainCamera = GameObject.Find("MixedRealityCameraParent");
    }

    // Update is called once per frame
    void Update()
    {
        pc.sp = speedSlider.SliderValue*0.5f;
        pc.collectivey = collectiveSlider.SliderValue*0.0004f;
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,
            sizeSlider.SliderValue*0.004f);
    }

    public void VoiceControlOnSlider(string voiceCommand)
    {
        switch (voiceCommand)
        {
            case "Faster":
                speedSlider.IncreaseSliderValue();
                break;
            case "Slower":
                speedSlider.DecreaseSliderValue();
                break;
            case "Bigger":
                sizeSlider.IncreaseSliderValue();
                break;
            case "Smaller":
                sizeSlider.DecreaseSliderValue();
                break;
            case "Coll_de":
                collectiveSlider.IncreaseSliderValue();
                break;
            case "Coll_in":
                collectiveSlider.DecreaseSliderValue();
                break;
        }
  
        /*
        if (voiceCommand == "Faster")
            speedSlider.IncreaseSliderValue();
        else if (voiceCommand == "Slower")
        {
            speedSlider.DecreaseSliderValue();
        }
        else if (voiceCommand == "Bigger")
        {
            sizeSlider.IncreaseSliderValue();
        }
        else if (voiceCommand == "Smaller")
        {
            sizeSlider.DecreaseSliderValue();
        }
        else if (voiceCommand == "Coll_de")
        {
            collectiveSlider.IncreaseSliderValue();
        }
        else if (voiceCommand == "Coll_in")
        {
            collectiveSlider.DecreaseSliderValue();
        }
        */
    }

    public void VoiceControlOnButton(string voiceCommand)
    {
        switch (voiceCommand)
        {
            case "WindFX_ON":
                airFlow.WindFX_ON();
                break;
            case "WindFX_OFF":
                airFlow.WindFX_OFF();
                break;
        }

    }

    public void VoiceControlOnSprite(string voiceCommand)
    {
        switch (voiceCommand)
        {
            case "FixedCam_ON":
                GameObject.Find("Panel").gameObject.SetActive(true);
                break;
            case "FixedCam_OFF":
                GameObject.Find("Panel").gameObject.SetActive(false);
                break;
        }
    }

    public void OnMakeFaster()
    {
        speedSlider.IncreaseSliderValue();
    }

    public void OnMakeSlower()
    {
        speedSlider.DecreaseSliderValue();
    }

    void Speed_slider()
    {
    }
}