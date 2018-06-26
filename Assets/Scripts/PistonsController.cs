using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PistonsController : MonoBehaviour {
	
	
	// UI - controls 
	public RadialSlider radialSlider;
	public Transform control_helper; 
	public Transform fixed_cam;
	public float collectivey = 0;
	public float slidersp = 0;
	public float collectivey_old = 0;
    public float slidersp_old = 0;
	public float sp_max = 500;
	
	public Vector3 fc_init;
	public float slider_fixedcam = 0;
	public float slider_fixedcam_min = -2.3f;
	public float slider_fixedcam_max = 2.3f;
	public float slider_fixedcam_old;

    public bool fix_angle;
    public float fix_speed;
    float lerpStart = 0;
    public float lerpEnd = 1.4f;
    float moveTime_up;
    float moveTime_down;
    // rotation 
    public Transform[] rotateObjects;
	public bool WindFx = false;
	public float speed = 300f;
	
	// define the piston points that push plate
	public Transform p1;
	public Transform p2;
	public Transform p3;
	public Transform targetObj;
		 
	// visualize plane, normals
	private Plane plane;
	private Mesh mesh;
	public float adjustSpeed = 30;
		
	// set limits to piston y movement
	public float y_init = 0;
	public float dy_max = 0.032f;
	public float dy_min = -0f;
	public float ang_max = 20f;

    Vector3 newfc_pos;
    Vector3 newfc_pos_old;

    public Vector3 targy;
	
	void Start () {
      
	    Vector3 _1 = p1.position;
        Vector3 _2 = p2.position;
        Vector3 _3 = p3.position;
                
		plane = new Plane(_1, _2, _3);
		
		targy = control_helper.position;
        y_init = targy.y;
        fc_init = fixed_cam.localPosition;
        
        

    }
	
	void Update () {

		Vector3 _1 = p1.position;
        Vector3 _2 = p2.position;
        Vector3 _3 = p3.position;
        
        if (control_helper){
			control_helper.rotation = Quaternion.Euler(0, radialSlider.ang, radialSlider.rad*ang_max);
			//targy.y = y_init + collectivey;
           // control_helper.position = Vector3.MoveTowards(control_helper.position, targy, 100);	
        }

        if (collectivey != collectivey_old)
        {
            control_helper.position = new Vector3(control_helper.position.x, control_helper.position.y + (collectivey - collectivey_old), control_helper.position.z);
        }


        plane.Set3Points(_1, _2, _3);


        // set plane
       // Vector3 newUp = Vector3.RotateTowards(targetObj.up, plane.normal, adjustSpeed * Mathf.Deg2Rad, 0);
        targetObj.rotation = Quaternion.FromToRotation(transform.up, plane.normal);

        //// move plate as plane moves
        Vector3 obj_pos = targetObj.position;
        obj_pos.y = control_helper.position.y;          //-1.0f * plane.distance;
        targetObj.position = obj_pos;

        moveTime_up += fix_speed * Time.deltaTime;
        moveTime_down += fix_speed * Time.deltaTime;
        if (fix_angle)
        {
            moveTime_down = 0;
            slider_fixedcam = Mathf.Lerp(lerpStart, lerpEnd, moveTime_up);

        }
        else
        {
            moveTime_up = 0;
            if (slider_fixedcam != 0)
            {
                slider_fixedcam = Mathf.Lerp(lerpEnd, lerpStart, moveTime_down);
                
            }
        }

        newfc_pos = fixed_cam.localPosition;
        newfc_pos.x = slider_fixedcam;
        newfc_pos_old = new Vector3(0,0,0);
        float theta;
        float theta_rounded;
        float _z_rounded;
        //if (newfc_pos != newfc_pos_old)
        //{
        theta = Vector3.Angle(newfc_pos, fc_init) - Vector3.Angle(fc_init, new Vector3(0, fc_init.y, 0));
        theta_rounded = (Mathf.Round(theta * 100)) / 100;
        float _z = Vector3.Distance(fc_init, new Vector3(0, fc_init.y, 0)) * Mathf.Sin(theta_rounded * Mathf.Deg2Rad);
        _z_rounded = (Mathf.Round(_z * 100)) / 100;
        newfc_pos.z = -Mathf.Abs(_z_rounded);
        //}


        Debug.Log(newfc_pos);
        //Debug.Log(newfc_pos_old);
        //Debug.Log(theta);



        fixed_cam.localPosition = Vector3.MoveTowards(fixed_cam.localPosition, newfc_pos, 100);
    }
	
	void LateUpdate(){
        DoRotate();
        newfc_pos_old = newfc_pos;
        collectivey_old = collectivey;
    }
		
	private void DoRotate(){
		
		foreach(Transform t in rotateObjects){
			t.localRotation = Quaternion.Euler(0, speed*Time.time, 0);	
		}
	}
		

	
	float DetectOnChange( float oldVal, float newVal ) {	
		
		
		return newVal;
	
	}

	
}
