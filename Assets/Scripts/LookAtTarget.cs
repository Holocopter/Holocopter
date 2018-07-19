using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {
	public Transform target;

    void Start() {
    }
	void Update () {

        //var lookPos = target.position - transform.position;
        //lookPos.z = lookPos.z*1;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * followSmooth);
        transform.LookAt(target);
    }
}
