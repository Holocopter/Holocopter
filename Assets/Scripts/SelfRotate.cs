using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour {

    public float amount_x;
    public float amount_y;
    public float amount_z;
    // Update is called once per frame
    void Update () {
		transform.Rotate(new Vector3(amount_x, amount_y, amount_z));
    }
}
