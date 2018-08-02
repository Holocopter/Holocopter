using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Examples.InteractiveElements;
using HoloToolkit.Sharing;

public class SlidersCommands : MonoBehaviour
{
    PistonsController pistonsController;
    GameObject SpeedSlider;
    GameObject CollectiveSlider;
    GameObject SizeSlier;
    GameObject AirFlow;

    bool SoundEffectPlayed;

    public GameObject FixedCamera;
    float start_camera_z;
    float sizeOld;
    int MaxSliderLevel =5;
    public RadialSlider radialSlider;

    public SliderGestureControl speedSlider;
    public SliderGestureControl collectiveSlider;
    public SliderGestureControl sizeSlider;
    public ButtonEvent airFlow;
    public VoiceCommondIndicator speedSliderIndicator;
    public VoiceCommondIndicator sizeSliderIndicator;


    public GameObject Sharing;

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

        Sharing = GameObject.Find("/Sharing");
        Sharing.SetActive(false);
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


    public void ShowServerMsg(long userId, string msgKey, List<float> msgs)
    {
        var floats = string.Join(", ", msgs.Select(x => x.ToString(CultureInfo.CurrentCulture)));
        Debug.Log($"{userId} said: {msgKey} change to {floats}");
    }

    public void MultiplayerVoiceControl(string command)
    {
        Sharing.SetActive(command == "start");
        FindObjectOfType<VoiceOverManager>().PlaySecondAudioClip();
        Debug.Log($"Muliplayer is now : {command == "start"}");
    }

    public void NetControlOnSlider(long userId, string msgKey, List<float> msgs)
    {
        Debug.Log($"[slider] Got {msgKey}");

        switch (msgKey)
        {
            case "CollectiveSlider":
                collectiveSlider.SetSliderValue(msgs[0]);
                break;
            case "SpeedSlider":
                speedSlider.SetSliderValue(msgs[0]);
                break;
            case "SizeSlider":
                sizeSlider.SetSliderValue(msgs[0]);
                break;
            case "RadialSlider":
                var pos = new Vector2(msgs[2], msgs[3]);
                radialSlider.SetAngRad(msgs[0], msgs[1], pos);
                break;
        }
    }

    public void VoiceControlOnSlider(string voiceCommand)
    {
        switch (voiceCommand)
        {
            case "Faster":
                speedSlider.IncreaseSliderValue();
                int speedSliderlevel_Faster = (int)((speedSlider.SliderValue / speedSlider.MaxSliderValue)*MaxSliderLevel);
                speedSliderIndicator.CurrentLevel = speedSliderlevel_Faster;
                break;
            case "Slower":
                speedSlider.DecreaseSliderValue();
                int speedSliderlevel_Slower = (int)((speedSlider.SliderValue / speedSlider.MaxSliderValue) * MaxSliderLevel);
                speedSliderIndicator.CurrentLevel = speedSliderlevel_Slower;
                break;
            case "Bigger":
                sizeSlider.IncreaseSliderValue();
                int sizeSliderlevel_Bigger = (int)((sizeSlider.SliderValue / sizeSlider.MaxSliderValue) * MaxSliderLevel);
                sizeSliderIndicator.CurrentLevel = sizeSliderlevel_Bigger;
                break;
            case "Smaller":
                sizeSlider.DecreaseSliderValue();
                int sizeSliderlevel_Smaller = (int)((sizeSlider.SliderValue / sizeSlider.MaxSliderValue) * MaxSliderLevel);
                sizeSliderIndicator.CurrentLevel = sizeSliderlevel_Smaller;
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
                GameObject.Find("FixedCamera").gameObject.SetActive(true);
                break;
            case "FixedCam_OFF":
                FixedCamera.gameObject.GetComponent<Image>().enabled = false;
                GameObject.Find("FixedCamera").gameObject.SetActive(false);
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