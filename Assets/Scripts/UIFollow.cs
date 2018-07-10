using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour {
    public Transform user;
    public Transform target;
    Quaternion initialRotation;
    // Use this for initialization
    void Start () {
        //store initial rotation
       initialRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(user.position.x, transform.position.y, user.position.z);
        transform.LookAt(target);
    }
}
