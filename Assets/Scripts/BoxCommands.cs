using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCommands : MonoBehaviour {

    PistonsController PC = GameObject.FindObjectOfType<PistonsController>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSelect() {
        PC.collectivey += 0.1f;
    }
}
