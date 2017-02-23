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
        STATE_REVERSAL,
        STATE_CRASH
    }

    public Vector3 _velocity;

    private Vector3 _move_force = new Vector3( 20, 0, 0 );
    private Vector3 _jump_force = new Vector3( 0, 50, 0 );
    private Vector3 _turbo_force = new Vector3( 200, 0, 0);

	private GameObject _input;
    private SEManager se;

    private float _hover_speed = 5.0f;
    private float _max_speed = 25.0f;
    private float _turbo_continue_max_time = 0.5f;
    private float _max_jump_force = 5.0f;

    private Vector3 _force = new Vector3( 0, 0, 0 );
    private STATE _state;
    private STATE _before_state;
	private bool _is_reversal = false;
    private bool _can_jump = true;
    private float _turbo_continue_time = -1;

    private bool _is_hit_debri = false;

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
		_input = GameObject.Find ("Input");
		if ( !_input ) {
			_input = ( GameObject )Instantiate( Resources.Load( "Prefab/Input" ) );
		}
        se = GetComponent<SEManager>( );
    }

	void Start ( ) {
        _turbo_continue_time = _turbo_continue_max_time * 60;
        _is_hit_debri = false;
	}
	
	// Update is called once per frame
	void Update ( ) {
        GameManager gm = GameObject.Find( "GameManager" ).GetComponent<GameManager>( );
        if ( gm.getPhase( ) == GameManager.PHASE.PHASE_PLAY ) {
            checkDeviceInput( );
        }
        moveUpdate( );
        switchState( );
        switchAnimation( );

        _velocity = GetComponent<Rigidbody>( ).velocity;
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
		InputManager input = _input.GetComponent< InputManager > ( );
        _force = new Vector3( 0, 0, 0 );
        Vector3 velocity = gameObject.GetComponent< Rigidbody >( ).velocity;
		if ( input.isHitKey( KeyCode.RightArrow ) ) {
            _force += _move_force;
        }

        bool is_fall = ( !( ( velocity.y > 0 && _jump_force.y > 0 ) ||
                         ( velocity.y < 0 && _jump_force.y < 0 ) ) && ( Mathf.Abs( velocity.y ) > 1.0f ) );
		
        if ( input.isHitKey( KeyCode.Z ) && !is_fall && Mathf.Abs( velocity.y ) < _max_jump_force && _can_jump ) {
            _force += _jump_force;
        }
        if ( Mathf.Abs( velocity.y ) >= _max_jump_force && !is_fall ) {
            _can_jump = false;
        }
        if (input.isHitKeyDown( KeyCode.X ) ) {
            _force = _turbo_force;
        }
		if ( input.isHitKeyDown( KeyCode.C )) {
			_jump_force.y *= -1;
			Physics.gravity = new Vector3 (0, Physics.gravity.y * -1, 0);
			transform.position = new Vector3 (transform.position.x, transform.position.y + (Physics.gravity.normalized.y), transform.position.z);
			transform.Rotate (Vector3.forward, 180);
			_is_reversal = true;
		} else {
			_is_reversal = false;
		}

        gameObject.GetComponent< Rigidbody >( ).AddForce( _force );
    }

    void switchState( ) {
		Animator anim = gameObject.GetComponent<Animator>( );
		AnimatorStateInfo anim_state = anim.GetCurrentAnimatorStateInfo( 0 );
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
		if ( _is_reversal ) {
			_state = STATE.STATE_REVERSAL;
		}

        bool is_fall = ( !is_jump ) && ( Mathf.Abs( velocity.y ) > 1.0f ) ;
        if ( is_fall ) {
            _state = STATE.STATE_FALL;
        }
		if ( anim_state.IsName( "Fall" ) && ( _state == STATE.STATE_WAIT || _state == STATE.STATE_RUN || _state == STATE.STATE_HOVER ) ) {
            _state = STATE.STATE_LAND;
            _can_jump = true;
        }
        if ( _force == _turbo_force || _turbo_continue_time < _turbo_continue_max_time * 60 ) {
            _state = STATE.STATE_TURBO;
        }
        if ( _before_state != STATE.STATE_TURBO && _state == STATE.STATE_TURBO ) {
            _turbo_continue_time = 0;
        }
        if ( _is_hit_debri ) {
            _state = STATE.STATE_CRASH;
        }
        //Debug.Log( velocity.y );
        _before_state = _state;
        _is_hit_debri = false;
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
                se.playSE( ( int )SEManager.SE.SE_TRUBO );                
                break;
			case STATE.STATE_REVERSAL:
				anim.SetBool( "isReversal", true );
                se.playSE( ( int )SEManager.SE.SE_GRAVITY );
				break;
            case STATE.STATE_CRASH:
                anim.SetBool("isHoverCrash", true);
                anim.SetBool("isCrash", true);
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
        anim.SetBool("isReversal", false);
        anim.SetBool("isHoverCrash", false);
        anim.SetBool("isCrash", false);
    }

    public bool isReversal( ) {
        return _jump_force.y < 0;
    }

    void OnCollisionEnter( Collision collision ) {
        if ( collision.collider.tag == "Debri"  ) {
            _is_hit_debri = true;
        }
    }
}
