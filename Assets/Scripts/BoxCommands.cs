using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCommands : MonoBehaviour {

	PistonsController PC;
	// Use this for initialization
	void Start () {
		PC = GameObject.FindObjectOfType<PistonsController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSelect() {
        PC.collectiveY += 0.1f;
		Debug.Log ("dasdas");
    }
}
