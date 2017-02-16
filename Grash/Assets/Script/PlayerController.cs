using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum STATE { 
        STATE_WAIT,
        STATE_RUN,
        STATE_HOVER,
        STATE_JUMP,
        STATE_FALL,
        STATE_LAND,
        STATE_TURBO,
        STATE_REVERSAL
    }

    private Vector3 _move_force = new Vector3( 20, 0, 0);
    private Vector3 _jump_force = new Vector3( 0, 20, 0 );
    private Vector3 _turbo_force = new Vector3( 30, 0, 0);

    private float _hover_speed = 5.0f;
    private float _max_speed = 30.0f;
    private float _turbo_continue_max_time = 0.5f;

    private Vector3 _force = new Vector3( 0, 0, 0 );
    private STATE _state;
    private STATE _before_state;
    private float _turbo_continue_time = -1;
   
    void Awake( ) {
        Rigidbody rigid = GetComponent<Rigidbody>( );
        if ( !rigid ) {
            gameObject.AddComponent<Rigidbody>( );
            rigid = GetComponent<Rigidbody>( );
        }
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Animator anim = GetComponent<Animator>( );
        if ( !anim ) {
            gameObject.AddComponent<Animator>( );
        }

    }

	void Start ( ) {
        _turbo_continue_time = _turbo_continue_max_time * 60;
	}
	
	// Update is called once per frame
	void Update ( ) {
        checkDeviceInput( );
        moveUpdate();
        switchState( );
        switchAnimation( );
        

	}

    void moveUpdate( ) { 
        Rigidbody rigid = gameObject.GetComponent< Rigidbody >( );
        Vector3 velocity = rigid.velocity;
        if ( velocity.x > _max_speed ) {
            velocity.x = _max_speed;
            rigid.velocity = velocity;
        }
        if ( _state == STATE.STATE_TURBO ) {
            rigid.useGravity = false;
        } else {
            rigid.useGravity = true;            
        }
        _turbo_continue_time++;
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
            transform.Rotate( Vector3.forward, 180 );
        }

        gameObject.GetComponent< Rigidbody >( ).AddForce( _force );
    }

    void switchState( ) {
      
        _state = STATE.STATE_WAIT;
        Vector3 velocity = gameObject.GetComponent< Rigidbody >( ).velocity;
        if ( velocity.x > 0 ) {
            _state = STATE.STATE_RUN;
        }
        if ( velocity.x > _hover_speed ) {
            _state = STATE.STATE_HOVER;
        }
        bool is_jump = ( ( velocity.y > 0 && _jump_force.y > 0 ) ||
                        ( velocity.y < 0 && _jump_force.y < 0 ) );
        if ( is_jump ) {
            _state = STATE.STATE_JUMP;
        }
        bool is_fall = ( !is_jump ) && ( Mathf.Abs( velocity.y ) > 1.0f ) ;
        if ( is_fall ) {
            _state = STATE.STATE_FALL;
        }
        Animator anim = gameObject.GetComponent<Animator>( );
        AnimatorStateInfo anim_state = anim.GetCurrentAnimatorStateInfo( 0 );
        if ( anim_state.IsName( "Fall" ) && _state != STATE.STATE_FALL) {
            _state = STATE.STATE_LAND;
        }
        if ( _force == _turbo_force || _turbo_continue_time < _turbo_continue_max_time * 60 ) {
            _state = STATE.STATE_TURBO;
        }
        if ( _before_state != STATE.STATE_TURBO && _state == STATE.STATE_TURBO ) {
            _turbo_continue_time = 0;
        }
        Debug.Log( velocity.y );
        _before_state = _state;
    }

    void switchAnimation( ) {
       
        Animator anim = gameObject.GetComponent<Animator>( );
        resetAnimation( );
        switch ( _state ) { 
            case STATE.STATE_RUN:
                anim.SetBool( "isRun", true );
                break;
            case STATE.STATE_HOVER:
                anim.SetBool( "isHover", true );
                break;
            case STATE.STATE_JUMP:
                anim.SetBool( "isJump", true );
                break;
            case STATE.STATE_FALL:
                anim.SetBool( "isFall", true );
                break;
            case STATE.STATE_LAND:
                anim.SetBool( "isLand", true );
                break;
            case STATE.STATE_TURBO:
                anim.SetBool( "isTurbo", true );
                break;
            default:
                resetAnimation( );
                break;
        }
    }

    void resetAnimation( ) {
        Animator anim = gameObject.GetComponent<Animator>( );
        anim.SetBool( "isRun", false );
        anim.SetBool( "isHover", false );
        anim.SetBool( "isJump", false );
        anim.SetBool( "isFall", false );
        anim.SetBool( "isLand", false );
        anim.SetBool( "isTurbo", false );


    }

}
