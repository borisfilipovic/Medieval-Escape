using UnityEngine;
using System.Collections;

public class GrabAndDrop : MonoBehaviour {

    public GameObject _placeholderLocation;

    private GameObject grabbedObject;
    private float grabbedObjectSize = 2.0f;
    private bool isHoldingObject = false;
	
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

        // 2. Positin object in front of player on _placeholder location transform.
        if (grabbedObject != null)
        {
            if (_placeholderLocation != null)
            {
                isHoldingObject = true; // Set flag to true, so that we can carry only one object.
                ResetGrabedObjectVelocity(); // Reset velocity.
                grabbedObject.transform.position = _placeholderLocation.transform.position; // Set grabed object positino to be in front of the camera.
                grabbedObject.transform.parent = GameObject.Find("FPSController").transform; // Set its parent.
                grabbedObject.transform.parent = GameObject.Find("FPSController").transform; // Set its parent again.
            }
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

    private void TryGrabObject(GameObject grabObject)
    {
        if(grabbedObject == null)
        {
            return;
        }

        grabbedObject = grabObject;

        // Get grabed object size.
        Renderer tmpRenderer = grabObject.GetComponent<Renderer>();
        if(tmpRenderer != null)
        {
            grabbedObjectSize = tmpRenderer.bounds.size.magnitude;
        }
    }

    private void DropObject()
    {
        if (grabbedObject == null)
            return;

        ResetGrabedObjectVelocity(); // Reset grabed object velocity, so it will not spin when we release it.

        grabbedObject.transform.parent = null; // Unparent it.
        grabbedObject = null; // Release object.
        isHoldingObject = false; // Reset flag, so that player can hold new object.

    }

    private void ResetGrabedObjectVelocity()
    {
        if (grabbedObject.GetComponent<Rigidbody>() != null)
        {
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

}
