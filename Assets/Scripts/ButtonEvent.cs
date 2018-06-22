using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour {

    PistonsController pistonsController;
    public GameObject IndicationBar;
    // Use this for initialization
    void Start()
    {
        pistonsController = GameObject.FindObjectOfType<PistonsController>();
        
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void WindFx_Switch()
    {
        switch (pistonsController.WindFx)
        {
            case true:
                pistonsController.WindFx = false;
                this.gameObject.GetComponentInChildren<Text>().text = "Airflow Effect: OFF";
                break;
            case false:
                pistonsController.WindFx = true;
                this.gameObject.GetComponentInChildren<Text>().text = "Airflow Effect: ON";
                break;
        }
    }

    public void WindFX_ON()
    {
        pistonsController.WindFx = true;
        this.gameObject.GetComponentInChildren<Text>().text = "Airflow Effect: ON";
    }

    public void WindFX_OFF() {
        pistonsController.WindFx = false;
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
