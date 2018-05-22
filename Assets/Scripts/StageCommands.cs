using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCommands : MonoBehaviour
{
    SpeechManager speechManager;
    
   

	// Use this for initialization
	void Start () {
        speechManager = GameObject.FindObjectOfType<SpeechManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnSelect() {
        speechManager.BroadcastMessage("OnReset");
    }
}
