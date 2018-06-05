using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersCommands : MonoBehaviour
{
    PistonsController pc;
    GameObject SpeedSlider;
    GameObject CollectiveSlider;
    GameObject SizeSlier;
    GameObject MainCamera;
    public Slider speedSlider;
    public Slider collectiveSlider;

    public Slider sizeSlider;

    private float sizeTargetValue = 0.0f;
    private float sizeCurValue = 0.0f;
    private bool animiDone = true;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindObjectOfType<PistonsController>();
        SpeedSlider = GameObject.Find("SpeedSlider");
        speedSlider = SpeedSlider.GetComponent<Slider>();
        CollectiveSlider = GameObject.Find("CollectiveSlider");
        collectiveSlider = CollectiveSlider.GetComponent<Slider>();
        SizeSlier = GameObject.Find("SizeSlider");
        sizeSlider = SizeSlier.GetComponent<Slider>();
        sizeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        MainCamera = GameObject.Find("MixedRealityCameraParent");

        sizeTargetValue = sizeCurValue = sizeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        pc.sp = speedSlider.value;
        pc.collectivey = collectiveSlider.value;
        MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,
            sizeSlider.value);

        sizeCurValue = Mathf.MoveTowards(sizeCurValue, sizeTargetValue, Time.deltaTime * 1);
        sizeSlider.value = sizeCurValue;
        if (Math.Abs(sizeCurValue - sizeTargetValue) < 0.01)
        {
            animiDone = true;
        }
    }

    public void ValueChangeCheck()
    {
        if (animiDone)
        {
            sizeTargetValue = sizeSlider.value;
            animiDone = false;
        }
    }

    void Speed_slider()
    {
    }
}