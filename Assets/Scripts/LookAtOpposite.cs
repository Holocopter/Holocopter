using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtOpposite : MonoBehaviour
{
    public Transform target;
    public float followSmooth = 1.0f;
    public GameObject UIPanel;
   // public Vector3 UIPanel;

    void Start()
    {
        UIPanel.transform.eulerAngles = new Vector3(-4.52f, 21.28f, -3.08f);
    }
    void Update()
    {

        //var lookPos = target.position - transform.position;
        //lookPos.z = 0;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * followSmooth);
        transform.LookAt(2 * transform.position - target.position);
       // transform.LookAt(target);
    }
}


