using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtOpposite : MonoBehaviour
{
    public Transform target;
    float i;
    public bool NeedBackToFace;
    public float followSmooth = 1.0f;

    void Start()
    {
        if (NeedBackToFace)
        {
            i = -transform.localRotation.z;
        }
        else
        {
            i = transform.localRotation.z;
        }
    }
    void Update()
    {

        //var lookPos = target.position - transform.position;
        //lookPos.z = lookPos.z*1;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * followSmooth);
        transform.LookAt(2 * transform.position - target.position);
    }
}


