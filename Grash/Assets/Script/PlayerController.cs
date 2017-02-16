using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
	// Use this for initialization

	Rigidbody rigitbody;

	void Start () {
		rigitbody = GetComponent< Rigidbody >();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKey( KeyCode.RightArrow ) ) {
			rigitbody.AddForce( new Vector3( 0, 0, 10 ) );
        }
		if ( Input.GetKey( KeyCode.LeftArrow ) ) {
			rigitbody.AddForce( new Vector3( 0, 0, -10 ) );
		}

		if ( Input.GetKeyDown( KeyCode.Space ) ) {
			rigitbody.AddForce( new Vector3( 0, 200, 0 ) );
		}
	}
}
