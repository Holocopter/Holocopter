using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Examples.InteractiveElements;

public class SlidersCommands : MonoBehaviour
{
    PistonsController pistonsController;
    GameObject SpeedSlider;
    GameObject CollectiveSlider;
    GameObject SizeSlier;
    GameObject AirFlow;
    public GameObject FixedCamera;
    GameObject MainCamera;
    float start_camera_z;
    public RadialSlider radialSlider;

    public SliderGestureControl speedSlider;
    public SliderGestureControl collectiveSlider;
    public SliderGestureControl sizeSlider;
    public ButtonEvent airFlow;

    // Use this for initialization
    void Start()
    {
        pistonsController = GameObject.FindObjectOfType<PistonsController>();
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
        pistonsController.speed = speedSlider.SliderValue * 0.5f;
        pistonsController.collectivey = collectiveSlider.SliderValue * 0.00004f;
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,
            sizeSlider.SliderValue * 0.002f);
    }


    public void ShowServerMsg(long userId, string msgContent)
    {
        Debug.Log(string.Format("{0} said: {1}", userId, msgContent));
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
                FixedCamera.gameObject.GetComponent<Image>().enabled = true;
                break;
            case "FixedCam_OFF":
                FixedCamera.gameObject.GetComponent<Image>().enabled = false;
                break;
        }
    }

    public void VoiceControlOnScene(string voiceCommand)
    {
        switch (voiceCommand)
        {
            case "Reset":
                speedSlider.SliderValue = 0;
                collectiveSlider.SliderValue = 0;
                sizeSlider.SliderValue = 0;
                radialSlider.ang = 0;
                radialSlider.rad = 0;
                radialSlider.ResetTheThrottle();
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