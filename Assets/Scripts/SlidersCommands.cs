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
    float sizeOld;
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
        AirFlow = GameObject.Find("windEffectSwitch");
        airFlow = AirFlow.GetComponent<ButtonEvent>();
        sizeOld = sizeSlider.SliderValue;
        MainCamera = GameObject.Find("MixedRealityCameraParent");
    }

    // Update is called once per frame
    void Update()
    {
        pistonsController.roterSpeed = speedSlider.SliderValue * 0.5f;
        pistonsController.collectiveY = collectiveSlider.SliderValue * 0.00004f;
        if (sizeOld != sizeSlider.SliderValue)
        {
            if (sizeOld < sizeSlider.SliderValue)
            {
                pistonsController.GetComponent<Transform>().localScale = new Vector3(pistonsController.rotorSize + 0.2f,
                    pistonsController.rotorSize + 0.2f, pistonsController.rotorSize + 0.2f);
            }
            else if (sizeOld > sizeSlider.SliderValue)
            {
                pistonsController.GetComponent<Transform>().localScale = new Vector3(pistonsController.rotorSize - 0.2f,
                    pistonsController.rotorSize - 0.2f, pistonsController.rotorSize - 0.2f);
            }

            sizeOld = sizeSlider.SliderValue;
        }
    }


    public void ShowServerMsg(long userId, string msgKey, string msgValue)
    {
        Debug.Log(string.Format("{0} said: {1} change to {2}", userId, msgKey, msgValue));
    }

    public void NetControlOnSlider(long userId, string msgKey, string msgValue)
    {
        Debug.Log(string.Format("Got {0} value {1}", msgKey, msgValue));

        switch (msgKey)
        {
            case "CollectiveSlider":
                collectiveSlider.SetSliderValue(float.Parse(msgValue));
                break;
            case "SpeedSlider":
                speedSlider.SetSliderValue(float.Parse(msgValue));
                break;
            case "SizeSlider":
                sizeSlider.SetSliderValue(float.Parse(msgValue));
                break;
            case "RadialSlider":
                var splited = msgValue.Split('_');
                var ang = float.Parse(splited[0]);
                var rad = float.Parse(splited[1]);
                var pos = new Vector2(float.Parse(splited[2]), float.Parse(splited[3]));
                Debug.Log(string.Format("{0} - {1}", ang, rad));
                radialSlider.SetAngRad(ang, rad, pos);
                break;
        }
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
            case "FixedCam_A":
                pistonsController.fixedCamAngleSwitch = false;
                break;
            case "FixedCam_B":
                pistonsController.fixedCamAngleSwitch = true;
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
                radialSlider.SetAngRad(0f, 0f);
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