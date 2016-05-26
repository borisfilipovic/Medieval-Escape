using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    public Animator animator;

    private Constants constClass;

    void Start() {
        InitConstClass();
    }

    void InitConstClass() {        
        constClass = new Constants(); // Init constant class.
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);

        if (constClass == null) {
            InitConstClass();
            return; 
        }

        if (col.gameObject.name == constClass.GetLvl1Keyname()) // Check if door and key collided.
        {
            Debug.Log("Sm notr?");
            animator.SetTrigger("OpenDoor"); // Start open door animation.
        }
    }
}
