using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCotroller : MonoBehaviour {

    GameObject player;
    Vector3 _player_to_camera_pos;

    void Awake( ) {
        player = GameObject.Find("player");
    }

	// Use this for initialization
	void Start () {
        _player_to_camera_pos = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        followPlayer( );
	}

    void followPlayer( ) {
        transform.position = player.transform.position + _player_to_camera_pos;
    }
}
