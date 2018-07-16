using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour {
    public Transform user;
    public Transform target;
    public float followSmooth;
    Quaternion initialRotation;
    // Use this for initialization
    void Start () {
        //store initial rotation
       initialRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
    }
	
	// Update is called once per frame
	void Update () {
        if(user!=null)
        transform.position = new Vector3(user.position.x, transform.position.y, user.position.z);
        if (target != null)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * followSmooth);
        }
    }
}
