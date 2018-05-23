using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCommands : MonoBehaviour {
    PistonsController pc;
    // Use this for initialization
    void Start () {
        pc = GameObject.FindObjectOfType<PistonsController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WINDFX() {
        pc.WindFx = true;
    }
    public void test() {
        Destroy(this.gameObject);
    }

    void OnSelect() {
        
    }
}
