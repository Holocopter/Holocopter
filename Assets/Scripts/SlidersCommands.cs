using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersCommands : MonoBehaviour
{
    PistonsController pc;

//    GameObject SpeedSlider;
//    GameObject CollectiveSlider;
//    GameObject SizeSlier;
    GameObject MainCamera;
    public Slider SpeedSlider;
    public Slider CollectiveSlider;
    public Slider SizeSlider;


    private SliderAnimation _speedAnimation;
    private SliderAnimation _sizeAnimation;
    private SliderAnimation _collectiveAnimation;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindObjectOfType<PistonsController>();
        SpeedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
        CollectiveSlider = GameObject.Find("CollectiveSlider").GetComponent<Slider>();
        SizeSlider = GameObject.Find("SizeSlider").GetComponent<Slider>();
        MainCamera = GameObject.Find("MixedRealityCameraParent");

        _speedAnimation = new SliderAnimation(SpeedSlider);
        _sizeAnimation = new SliderAnimation(SizeSlider);
        _collectiveAnimation = new SliderAnimation(CollectiveSlider);
    }

    // Update is called once per frame
    void Update()
    {
        pc.sp = SpeedSlider.value;
        pc.collectivey = CollectiveSlider.value;
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,
            SizeSlider.value);


        _speedAnimation.UpdatePos();
        _sizeAnimation.UpdatePos();
        _collectiveAnimation.UpdatePos();
    }


    void Speed_slider()
    {
    }
}

public class SliderAnimation
{
    public float TargetValue { get; private set; }
    public float CurValue { get; private set; }
    public bool AnimiDone { get; private set; }
    public Slider Slider { get; private set; }
    private readonly float _sliderAnimiSpeed;
    private readonly double _tolerance;


    public SliderAnimation(Slider slider)
    {
        this.Slider = slider;
        TargetValue = CurValue = slider.value;
        AnimiDone = true;
        Slider.onValueChanged.AddListener(delegate { OnValueChange(); });
        _sliderAnimiSpeed = (Slider.maxValue - Slider.minValue) / 3;
        _tolerance = Math.Min(0.01, (Slider.maxValue - Slider.minValue) / 100);
    }

    public void UpdatePos()
    {
        if (AnimiDone) return;
        CurValue = Mathf.MoveTowards(CurValue, TargetValue, Time.deltaTime * _sliderAnimiSpeed);
        Slider.value = CurValue;
        if (Math.Abs(CurValue - TargetValue) < _tolerance)
        {
            AnimiDone = true;
        }
    }

    private void OnValueChange()
    {
        if (!this.AnimiDone) return;
        TargetValue = Slider.value;
        AnimiDone = false;
    }
}