using UnityEngine;
using System.Collections;

public class GrabAndDrop : MonoBehaviour {

    /* PUBLIC VARIABLES */
    public GameObject _placeholderLocation;

    /* PRIVATE VARIABLES */
    private GameObject grabbedObject;
    private float grabbedObjectSize = 2.0f;
    private bool isHoldingObject = false;
    private Constants constantClass;

    void Start() {
        InitConstantClass();
    }

    private void InitConstantClass() {
        constantClass = new Constants();
    }
	
	// Update is called once per frame
	void Update () {    
        // Check if we are clicking.
        if (Input.GetMouseButtonDown(0))
        {
            if(grabbedObject == null)
            {
                if (!isHoldingObject) // Check if we are already holding an object.
                {
                    GrabObject(); // Grab object.
                }
            }
            else
            {
                if (isHoldingObject)
                {
                    DropObject(); // Drop object.                                       
                }                
            }
        }
    }

    private void GrabObject()
    {
        // 1. Grab object.
        grabbedObject = GetMouseHoverObject(5);

        if(constantClass != null)
        {
            InitConstantClass();
        }

        // 2. Positin object in front of player on _placeholder location transform.
        if (grabbedObject != null && constantClass != null && grabbedObject.tag == constantClass.GetGrabbableTag()) // Check if object have grabbable tag.
        {
            if (_placeholderLocation != null)
            {
                Debug.Log(grabbedObject.tag);
                isHoldingObject = true; // Set flag to true, so that we can carry only one object.
                ResetGrabedObjectVelocity(); // Reset velocity.
                grabbedObject.transform.position = _placeholderLocation.transform.position; // Set grabed object positino to be in front of the camera.
                grabbedObject.transform.parent = GameObject.Find("FPSController").transform; // Set its parent.
                grabbedObject.transform.parent = GameObject.Find("FPSController").transform; // Set its parent again.

                // Remove rigidbody from grabbed object.
                HandleGrabedObjectRigidbody(true);
            }
        }
        else
        {
            DropObject();
        }
    }

    GameObject GetMouseHoverObject(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit raycastHit;
        Vector3 target = position + Camera.main.transform.forward * range;

        if (Physics.Linecast(position, target, out raycastHit))
        {
            return raycastHit.collider.gameObject;
        }
        else {
            return null;
        }
    }

    private void DropObject()
    {
        if (grabbedObject == null)
            return;

        ResetGrabedObjectVelocity(); // Reset grabed object velocity, so it will not spin when we release it.

        // Add rigidbody to grabbed object.
        HandleGrabedObjectRigidbody(false);

        grabbedObject.transform.parent = null; // Unparent it.
        grabbedObject = null; // Release object.
        isHoldingObject = false; // Reset flag, so that player can hold new object.
    }

    private void HandleGrabedObjectRigidbody(bool bool_value)
    {
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = !bool_value;
            rb.freezeRotation = bool_value;
            rb.drag = !bool_value ? 0.0f : 100.0f; // If bool_value == false, that means that we droped object, so lets set drag value to 0 again to that object will fall to floor.
        }

        /*BoxCollider bc = grabbedObject.GetComponent<BoxCollider>();
        if(bc != null)
        {
            bc.enabled = !bool_value;
        }*/
    }

    private void ResetGrabedObjectVelocity()
    {        
        if (grabbedObject.GetComponent<Rigidbody>() != null)
        {
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }   
}
