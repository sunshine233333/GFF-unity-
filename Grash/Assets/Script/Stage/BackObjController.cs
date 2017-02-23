using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObjController : MonoBehaviour {

    private const float ROTATE_VALUE = 0.1f;

    private GameObject _back_obj;
    private Rigidbody _player;


	// Use this for initialization
	void Start ( ) {
        _back_obj = GetComponent<GameObject>( );
		_player = GameObject.Find( "Player" ).GetComponent<Rigidbody>( );
	}
	
	// Update is called once per frame
	void Update ( ) {
		Vector3 player_force = _player.velocity;
        if ( player_force.x == 0.0f ) {
            return;
        }
        FollowPlayer( player_force.x );
        RotateObj( );
	}

    private void FollowPlayer( float value ) {
        Vector3 _back_obj_pos = _back_obj.transform.position;
        _back_obj_pos.x += value;
        _back_obj.transform.position = _back_obj_pos;
    }

    private void RotateObj( ) {
        _back_obj.transform.Rotate( new Vector3( 0, ROTATE_VALUE, 0 ) );
    }
}
