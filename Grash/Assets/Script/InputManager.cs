using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	[SerializeField]
	private bool _hit_right_arrow = false;
	[SerializeField]
	private bool _hit_key_z = false;
	[SerializeField]
	private bool _hit_key_x = false;
	[SerializeField]
	private bool _hit_key_c = false;
	[SerializeField]
	private bool _before_hit_right_arrow = false;
	[SerializeField]
	private bool _before_hit_key_z = false;
	[SerializeField]
	private bool _before_hit_key_x = false;
	[SerializeField]
	private bool _before_hit_key_c = false;

	private float _time = 0.0f;
	private float _max_time = 1.0f;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		_time++;
		if ( _max_time > _time ) {
			return;
		}
		_time = 0;
		_before_hit_right_arrow = _hit_right_arrow;
		_before_hit_key_z = _hit_key_z;
		_before_hit_key_c = _hit_key_c;
		_before_hit_key_x = _hit_key_x;
		_hit_key_x = false;
		_hit_key_c = false;
	}

	public void hitRightArrow( ) {
		_hit_right_arrow = true;
	}

	public void hitRightArrowStop( ) {
		_hit_right_arrow = false;
	}

	public void hitKeyZStop( ) {
		_hit_key_z = false;
	}

	public void hitKeyZ( ) {
		_hit_key_z = true;
	}

	public void hitKeyX( ) {
		_hit_key_x = true;
	}

	public void hitKeyC( ) {
		_hit_key_c = true;
	}

	public bool isHitKey( KeyCode key ) {
		bool touch_key = false;
		switch ( key ) {
		case KeyCode.RightArrow:
			touch_key = _hit_right_arrow;
			break;
		case KeyCode.Z:
			touch_key = _hit_key_z;
			break;
		case KeyCode.X:
			touch_key = _hit_key_x;
			break;
		case KeyCode.C:
			touch_key = _hit_key_c;
			break;
		default:
			break;
		}
		Debug.Log (touch_key);
		return Input.GetKey ( key ) || touch_key;
	}
	public bool isHitKeyDown( KeyCode key ) {
		bool before_key = false;
		bool touch_key = false;
		switch ( key ) {
		case KeyCode.RightArrow:
			touch_key = _hit_right_arrow;
			before_key = _before_hit_right_arrow;
			break;
		case KeyCode.Z:
			touch_key = _hit_key_z;
			before_key = _before_hit_key_z;
			break;
		case KeyCode.X:
			touch_key = _hit_key_x;
			before_key = _before_hit_key_x;
			break;
		case KeyCode.C:
			touch_key = _hit_key_c;
			before_key = _before_hit_key_c;
			break;
		default:
			break;
		}
		return Input.GetKeyDown ( key ) || ( touch_key && !before_key );
	}
}
