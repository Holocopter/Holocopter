using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour {

    public Transform Target_transform;
    Transform old_transform;
	// Use this for initialization
	void Start () {
        old_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(Target_transform.position.x, Target_transform.position.y, Target_transform.position.z);
        Debug.Log(this.transform.position);
        Debug.Log(old_transform);
	}
}
