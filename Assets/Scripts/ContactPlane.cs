using UnityEngine;

public class ContactPlane : MonoBehaviour {
	
	private Vector3 newPos;
	private RaycastHit hit;
    public LayerMask layerMask;

    void Start()
    {
        newPos = transform.position;
    }

    void Update()
    {
        newPos = transform.position;
        Vector3 dir = transform.TransformDirection(Vector3.up);
        if (Physics.Raycast(transform.position, dir, out hit, 50,layerMask) ||
             Physics.Raycast(transform.position, -dir, out hit, 50, layerMask))
        {

            newPos.y = hit.point.y;
            transform.position = newPos;

            //  print(hit.point.y + " hit " + hit.distance);

        }

    }
    
}
