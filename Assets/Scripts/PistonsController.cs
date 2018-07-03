using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PistonsController : MonoBehaviour {
	
	
	// UI - controls 
	public RadialSlider radialSlider;
	public Transform controlHelper; 
	public Transform fixedCam;
	public float collectiveY = 0;
	public float collectiveY_Old = 0;

	
	public Vector3 fixCamInitalPosition;
	public float fixedCamAngle = 0;
	public float fixedCamAngle_Min = -2.3f;
	public float fixedCamAngle_Max = 2.3f;
	public float fixedCamAngle_Old;

    public bool fixedCamAngleSwitch;
    public float fixedCamAngleChangeSpeed;
    float lerpStart = 0;
    public float lerpEnd = 1.4f;
    float lerpTime_Up;
    float lerpTime_Down;
    // rotation 
    public Transform[] rotateObjects;
	public bool windEffectSwitch = false;
	public float roterSpeed = 300f;
	
	// define the piston points that push plate
	public Transform pistonPoint1;
	public Transform pistonPoint2;
	public Transform pistonPoint3;
	public Transform transparentPlate;

    public float rotorSize;

    // visualize virturalPlane, normals
    private Plane virturalPlane;
		
	// set limits to piston y movement
	public float controlHelperInitialY = 0;
	public float controlHelperRotationAngle_Max = 20f;

    Vector3 fixedCamPosition;
    Vector3 fixedCamPosition_Old;

    public Vector3 controlHelperPosition;
	
	void Start () {
      
	    Vector3 _1 = pistonPoint1.position;
        Vector3 _2 = pistonPoint2.position;
        Vector3 _3 = pistonPoint3.position;
                
		virturalPlane = new Plane(_1, _2, _3);
		
		controlHelperPosition = controlHelper.position;
        controlHelperInitialY = controlHelperPosition.y;
        fixCamInitalPosition = fixedCam.localPosition;
        
        

    }
	
	void Update () {

		Vector3 _1 = pistonPoint1.position;
        Vector3 _2 = pistonPoint2.position;
        Vector3 _3 = pistonPoint3.position;
        
        if (controlHelper){
			controlHelper.rotation = Quaternion.Euler(0, radialSlider.ang, radialSlider.rad*controlHelperRotationAngle_Max);
			//controlHelperPosition.y = controlHelperInitialY + collectiveY;
           // controlHelper.position = Vector3.MoveTowards(controlHelper.position, controlHelperPosition, 100);	
        }

        if (collectiveY != collectiveY_Old)
        {
            controlHelper.position = new Vector3(controlHelper.position.x, controlHelper.position.y + (collectiveY - collectiveY_Old), controlHelper.position.z);
        }


        virturalPlane.Set3Points(_1, _2, _3);

	    rotorSize = transform.localScale.x;

        // set virturalPlane
        // Vector3 newUp = Vector3.RotateTowards(transparentPlate.up, virturalPlane.normal, adjustSpeed * Mathf.Deg2Rad, 0);
        transparentPlate.rotation = Quaternion.FromToRotation(transform.up, virturalPlane.normal);

        //// move plate as virturalPlane moves
        Vector3 obj_pos = transparentPlate.position;
        obj_pos.y = controlHelper.position.y;          //-1.0f * virturalPlane.distance;
        transparentPlate.position = obj_pos;

        lerpTime_Up += fixedCamAngleChangeSpeed * Time.deltaTime;
        lerpTime_Down += fixedCamAngleChangeSpeed * Time.deltaTime;
        if (fixedCamAngleSwitch)
        {
            lerpTime_Down = 0;
            fixedCamAngle = Mathf.Lerp(lerpStart, lerpEnd, lerpTime_Up);

        }
        else
        {
            lerpTime_Up = 0;
            if (fixedCamAngle != 0)
            {
                fixedCamAngle = Mathf.Lerp(lerpEnd, lerpStart, lerpTime_Down);
                
            }
        }

        fixedCamPosition = fixedCam.localPosition;
        fixedCamPosition.x = fixedCamAngle;
        fixedCamPosition_Old = new Vector3(0,0,0);
        float theta;
        float theta_rounded;
        float _z_rounded;

        theta = Vector3.Angle(fixedCamPosition, fixCamInitalPosition) - Vector3.Angle(fixCamInitalPosition, new Vector3(0, fixCamInitalPosition.y, 0));
        theta_rounded = (Mathf.Round(theta * 100)) / 100;
        float _z = Vector3.Distance(fixCamInitalPosition, new Vector3(0, fixCamInitalPosition.y, 0)) * Mathf.Sin(theta_rounded * Mathf.Deg2Rad);
        _z_rounded = (Mathf.Round(_z * 100)) / 100;
        fixedCamPosition.z = -Mathf.Abs(_z_rounded);
        fixedCam.localPosition = Vector3.MoveTowards(fixedCam.localPosition, fixedCamPosition, 100);
    }
	
	void LateUpdate(){
        DoRotate();
        fixedCamPosition_Old = fixedCamPosition;
        collectiveY_Old = collectiveY;
    }
		
	private void DoRotate(){
		
		foreach(Transform t in rotateObjects){
			t.localRotation = Quaternion.Euler(0, roterSpeed*Time.time, 0);	
		}
	}
		

	
	float DetectOnChange( float oldVal, float newVal ) {	
		
		
		return newVal;
	
	}

	
}
