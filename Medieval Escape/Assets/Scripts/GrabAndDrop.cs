using UnityEngine;
using System.Collections;

public class GrabAndDrop : MonoBehaviour {

    private GameObject grabbedObject;
    private float grabbedObjectSize = 2.0f;
	
	// Update is called once per frame
	void Update () {
        Debug.Log(GetMouseHoverObject(5));

        // Check if we are clicking.
        if (Input.GetMouseButtonDown(0))
        {
            if(grabbedObject == null)
            {
                Debug.Log("1");
                TryGrabObject(GetMouseHoverObject(50));
            }
            else
            {
                Debug.Log("2");
                DropObject();
            }
        }

        if(grabbedObject != null)
        {
            Vector3 newPosition = gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize; // Our new position.
            grabbedObject.transform.position = newPosition;
        }
	}

    bool CanGrab(GameObject candidate)
    {
        return candidate.GetComponent<Rigidbody>() != null; // Če ima objekt rigidbody, potem ga lahko grabamo.
    }

    GameObject GetMouseHoverObject(float range)
    {
        Vector3 position = gameObject.transform.position;
        RaycastHit raycastHit;
        Vector3 target = position + Camera.main.transform.forward;
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
        Debug.Log("Did we grab something?");
        if(grabbedObject == null)
        {
            return;
        }
        Debug.Log("secondpart");
        grabbedObject = grabObject;
        //grabbedObjectSize = grabObject.renderer.bounds.size.magnitude;
    }

    private void DropObject()
    {
        if (grabbedObject == null)
            return;

        if(grabbedObject.GetComponent<Rigidbody>() != null)
        {
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        grabbedObject = null; // Release object.
    }

}
