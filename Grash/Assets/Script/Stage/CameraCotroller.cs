using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCotroller : MonoBehaviour {

    GameObject _player;
    Vector3 _player_to_camera_pos;

    void Awake( ) {
        _player = GameObject.Find("Player");
    }

	// Use this for initialization
	void Start () {
        _player_to_camera_pos = transform.position - _player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        followPlayer( );
	}

    void followPlayer( ) {
        transform.position = _player.transform.position + _player_to_camera_pos;
    }
}
