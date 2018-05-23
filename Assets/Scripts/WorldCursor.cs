using UnityEngine;

public class WorldCursor : MonoBehaviour
{
	private MeshRenderer meshRenderer;
    Transform oldLocation;
    // Use this for initialization
    void Awake() {
       // oldLocation.position = new Vector3(0f, 0.8f, -1.85f);
    }
	void Start()
	{
        
		// Grab the mesh renderer that's on the same object as this script.
		meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
       
    }

	// Update is called once per frame
	void Update()
	{
		// Do a raycast into the world based on the user's
		// head position and orientation.
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;

		if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
		{
			// If the raycast hit a hologram...
			// Display the cursor mesh.
			meshRenderer.enabled = true;

			// Move the cursor to the point where the raycast hit.
			this.transform.position = hitInfo.point;

			// Rotate the cursor to hug the surface of the hologram.
			this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		}
        
		else
		{
            // If the raycast did not hit a hologram, hide the cursor mesh.
             meshRenderer.enabled = false;
            // this.transform.position = new Vector3(headPosition.x, headPosition.y, headPosition.z + 0.1f);
           // this.transform.position = oldLocation.position;
		}
	}
}