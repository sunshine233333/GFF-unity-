using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObjController : MonoBehaviour {

    private const float ROTATE_VALUE = 0.5f;

    private GameObject _player;
    private Vector3 _player_to_backobj_pos;

    private void Awake( ) {
        _player = GameObject.Find( "Player" );
    }

    // Use this for initialization
    void Start ( ) {
        _player_to_backobj_pos = _player.transform.position + transform.position;
	}
	
	// Update is called once per frame
	void Update ( ) {
        FollowPlayer( );
        RotateObj( );
    }

    private void FollowPlayer( ) {
        Vector3 pos = new Vector3( _player.transform.position.x + _player_to_backobj_pos.x, _player_to_backobj_pos.y, _player_to_backobj_pos.z );
		transform.position = pos;
    }
    private void RotateObj( ) {
        if ( _player.GetComponent<Rigidbody>( ).velocity.x == 0 ) {
            return;
        }
        transform.Rotate( new Vector3( 0, ROTATE_VALUE, 0 ) );
    }

}
