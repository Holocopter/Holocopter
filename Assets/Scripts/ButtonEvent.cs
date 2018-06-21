using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour {

    PistonsController pc;
    public GameObject IndicationBar;
    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindObjectOfType<PistonsController>();
        
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void WindFx_Switch()
    {
        switch (pc.WindFx)
        {
            case true:
                pc.WindFx = false;
                this.gameObject.GetComponentInChildren<Text>().text = "Airflow Effect: OFF";
                break;
            case false:
                pc.WindFx = true;
                this.gameObject.GetComponentInChildren<Text>().text = "Airflow Effect: ON";
                break;
        }
    }

    public void WindFX_ON()
    {
        pc.WindFx = true;
        this.gameObject.GetComponentInChildren<Text>().text = "Airflow Effect: ON";
    }

    public void WindFX_OFF() {
        pc.WindFx = false;
        this.gameObject.GetComponentInChildren<Text>().text = "Airflow Effect: OFF";
    }
    public void ButtonIndicationEnter()
    {
        //IndicationBar = this.transform.Find("IndicationBar").gameObject;

        if (IndicationBar != null)
        {
            IndicationBar.SetActive(true);
        }


    }

    public void ButtonIndicationExit()
    {
        // IndicationBar = this.transform.Find("IndicationBar").gameObject;

        if (IndicationBar != null)
        {
            IndicationBar.SetActive(false);
        }


    }
}
