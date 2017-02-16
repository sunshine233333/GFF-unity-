using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum STATE { 
        STATE_WAIT,
        STATE_RUN,
        STATE_JUMP,
        STATE_FALL,
    }

    private Vector3 _move_force = new Vector3( 20, 0, 0);
    private Vector3 _jump_force = new Vector3( 0, 20, 0 );
    private Vector3 _turbo_force = new Vector3( 30, 0, 0);

    private Vector3 _force = new Vector3( 0, 0, 0 );
    private Vector3 _before_pos = new Vector3( 0, 0, 0 );
    private STATE _state;
    void Awake( ) {
        Rigidbody rigid = GetComponent<Rigidbody>( );
        if ( !rigid ) {
            gameObject.AddComponent<Rigidbody>();
            rigid = GetComponent<Rigidbody>();
        }
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        Animator anim = GetComponent<Animator>( );
        if ( !anim ) {
            gameObject.AddComponent<Animator>();
        }

    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _before_pos = transform.position;
        checkDeviceInput( );
        switchState( );
        switchAnimation( );
	}

    void checkDeviceInput( ) {
        _force = new Vector3( 0, 0, 0 );
        if ( Input.GetKey( KeyCode.RightArrow ) ) {
            _force += _move_force;
        }
        if ( Input.GetKey( KeyCode.Z ) ) {
            _force += _jump_force;
        }
        if ( Input.GetKey( KeyCode.X ) ) {
            _force = _turbo_force;
        }
        if ( Input.GetKeyDown( KeyCode.C ) ) {
            _jump_force.y *= -1;
            Physics.gravity = new Vector3( 0, Physics.gravity.y * -1, 0 );
        }

        gameObject.GetComponent< Rigidbody >( ).AddForce( _force );
    }

    void switchState( ) {
        _state = STATE.STATE_WAIT;
        Vector3 pos = transform.position;
        Vector3 move_lenght = pos - _before_pos;
        if ( _force.x > 0 ) {
            _state = STATE.STATE_RUN;
        }
    }

    void switchAnimation() {
       
        Animator anim = gameObject.GetComponent<Animator>( );
        switch ( _state ) { 
            case STATE.STATE_RUN:
                anim.SetBool( "isRun", true );
                break;
            default:
                resetAnimation();
                break;
        }
    }

    void resetAnimation( ) {
        Animator anim = gameObject.GetComponent<Animator>( );
        anim.SetBool( "isRun", false );
    }

}
