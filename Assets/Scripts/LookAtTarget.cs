using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {
	public Transform target;
    float i;
    public bool NeedBackToFace;
    public float followSmooth = 1.0f;

    void Start() {
        if (NeedBackToFace)
        {
            i = -transform.localRotation.z;
        }
        else
        {
            i = transform.localRotation.z;
        }
    }
	void Update () {

        //var lookPos = target.position - transform.position;
        //lookPos.z = lookPos.z*1;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * followSmooth);
        transform.LookAt(target);
    }
}
